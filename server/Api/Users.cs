using Alaska.Data;
using Alaska.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Api
{
    internal static class Users
    {
        internal static void MapUserEndPoints(this WebApplication app)
        {
            app.MapGet("/user-manager/", GelAllAsync).RequireAuthorization();
            app.MapGet("/user-manager/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/user-manager/", CreateAsync).RequireAuthorization();
            app.MapPut("/user-manager/", UpdateAsync).RequireAuthorization();
            app.MapDelete("/user-manager/{id}", DeleteAsync).RequireAuthorization();
            app.MapPost("/user-manager/reset-password/", UpdatePasswordAsync).RequireAuthorization();
        }
        internal static async Task<IResult> GelAllAsync(HttpContext context, DbClient db)
        {
            string commandText = """
                SELECT u.id, u.[name], CASE WHEN u.[role] = -1 THEN 'Superuser' ELSE  ISNULL(r.[name], '-') END AS roleName, CASE WHEN u.createdBy = -1 THEN 'System' ELSE ISNULL(c.[name], '-') END AS createdBy, u.createdAt
                FROM users AS u
                LEFT JOIN roles AS r ON u.[role] = r.id
                LEFT JOIN users AS c ON u.createdBy = c.[id]
                WHERE u.deleted = 0
                """;
            byte[] data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    if (reader is null) return;
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteString(reader.GetString(1));
                        builder.WriteString(reader.GetString(2));
                        builder.WriteString(reader.GetString(3));
                        builder.WriteDateTime(reader.GetDateTime(4));
                    }
                    data = builder.ToArray();
                }, commandText);
                return Results.File(data, "application/octet-stream", "users.bin");
            }
        }
        internal static async Task<IResult> GetByIdAsync(HttpContext context, DbClient db, int id)
        {
            var uvm = new UserViewModel();
            var commandText = "SELECT [id], [name] FROM roles WHERE deleted = 0 ORDER BY [id]";
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (reader is null) return;
                while (await reader.ReadAsync())
                {
                    uvm.Roles.Add(new Role() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }, commandText);
            commandText = "SELECT [id], [name], [role], [login] FROM users WHERE [deleted] = 0 AND id=@id";
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (reader is null) return;
                if (await reader.ReadAsync())
                {
                    uvm.User = new User()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        RoleId = reader.GetInt32(2),
                        Login = reader.GetString(3)
                    };
                }
            }, commandText, new SqlParameter("@id", id));
            return Results.Ok(uvm);
        }
        internal static async Task<IResult> CreateAsync(HttpContext context, DbClient db, User user)
        {
            var commandText = """
                INSERT INTO users ([name], [login], [password], [role], [createdBy])
                VALUES (@name, @login, HASHBYTES('SHA2_256', @password), @role, @createdBy);
                """;
            var (userID, roleID) = AppHelpers.ExtractPrincipal(context);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", user.Name),
                new SqlParameter("@login", user.Login),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@role", user.RoleId),
                new SqlParameter("@createdBy", userID)
            };
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, parameters));
        }
        internal static async Task<IResult> UpdateAsync(HttpContext context, DbClient db, User user)
        {
            var commandText = """
                UPDATE users 
                SET [name] = @name, [role] = @role, [password] = HASHBYTES('SHA2_256', @password), editedBy=@editedBy
                WHERE [id]=@id
                """;
            var userId = AppHelpers.GetUserID(context);
            var parameter = new SqlParameter[]
            {
                new SqlParameter("@name", user.Name),
                new SqlParameter("@role", user.RoleId),
                new SqlParameter("@editedBy", userId),
                new SqlParameter("@id", user.Id),
                new SqlParameter("@password", user.Password)
            };
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, parameter)) ;
        }
        internal static async Task<IResult> DeleteAsync(HttpContext context, DbClient db, int id)
        {
            var commandText = "UPDATE users SET deleted = 1, editedBy = @editedBy WHERE id=@id";
            var userId = AppHelpers.GetUserID(context);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@editedBy", userId),
                new SqlParameter("@id", id)
            };
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, parameters));
        }
        internal static async Task<IResult> UpdatePasswordAsync(ResetPasswordModel model, DbClient db, HttpContext context)
        {
            var commandText = "SELECT 1 FROM users WHERE id = @id AND [password] = HASHBYTES('SHA2_256', @oldpassword)";
            var oldPasswordMatch = await db.AnyRecords(commandText, new SqlParameter("@id", model.UserID), new SqlParameter("@oldpassword", model.OldPassword));
            if (!oldPasswordMatch)
            {
                return Results.Ok(new CommonResult() { Success = false, Message = "Password lama tidak valid" });
            }
            commandText = "UPDATE users SET [password] = @newpassword WHERE [id] = @id";
            var success = await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", model.UserID), new SqlParameter("@newpassword", model.NewPassword));
            return Results.Ok(success);
        }
    }
}
