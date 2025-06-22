using Alaska.Data;
using Alaska.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Api
{
    internal static class Roles
    {
        internal static void MapRoleEndPoints(this WebApplication app)
        {
            app.MapGet("/role-manager", GetAllAsync).RequireAuthorization();
            app.MapGet("/role-manager/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/role-manager", CreateAsync).RequireAuthorization();
            app.MapPut("/role-manager", UpdateAsync).RequireAuthorization();
            app.MapDelete("/role-manager/{id}", DeleteAsync).RequireAuthorization();
            app.MapPost("/role-manager/exists", ExistAsync).RequireAuthorization();
        }
        internal static async Task<IResult> GetAllAsync(DbClient db)
        {
            var commandText = """
                SELECT r.id, r.[name], u.[name] AS creator, r.createdDate
                FROM roles AS r WITH (NOLOCK)
                INNER JOIN users AS u ON r.createdBy = u.id
                WHERE r.deleted = 0
                """;
            byte[] data = Array.Empty<byte>();
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                using (var builder = new BinaryBuilder())
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteString(reader.GetString(1));
                        builder.WriteString(reader.GetString(2));
                        builder.WriteDateTime(reader.GetDateTime(3));
                    }
                    data = builder.ToArray();
                }
            }, commandText);
            return Results.File(data, "application/octet-stream");
        }
        internal static async Task<IResult> GetByIdAsync(int id, HttpContext context, DbClient db)
        {
            var commandText = "SELECT [id], [name] FROM roles WHERE id=@id";
            Role? role = null;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    role = new Role()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }, commandText, new SqlParameter("@id", id));
            if (role != null) return Results.Ok(role);
            return Results.NotFound();
        }
        internal static async Task<IResult> CreateAsync(Role role, HttpContext context, DbClient db)
        {
            var commandText = """
                INSERT INTO roles 
                ([name],[createdBy])
                VALUES
                (@name, @createdBy)
                """;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", role.Name),
                new SqlParameter("@createdBy", AppHelpers.GetUserID(context))
            };
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, parameters));
        }
        internal static async Task<IResult> UpdateAsync(Role role, HttpContext context, DbClient db)
        {
            var commandText = """
                UPDATE roles 
                SET [name]=@name,
                [editedBy]=@editor
                WHERE id=@id
                """;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", role.Name),
                new SqlParameter("@editor", AppHelpers.GetUserID(context)),
                new SqlParameter("@id", role.Id)
            };
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, parameters));
        }
        internal static async Task<IResult> DeleteAsync(int id, HttpContext context, DbClient db)
        {
            var commandText = "UPDATE roles SET deleted = 1 WHERE id=@id";
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id), new SqlParameter("@editor", AppHelpers.GetUserID(context))));
        }
        internal static async Task<IResult> ExistAsync(Role role, DbClient db)
        {
            var commandText = "SELECT 1 FROM roles WHERE [name] = @name AND NOT [id] = @id";
            var exists = await db.AnyRecords(commandText, new SqlParameter("@name", role.Name), new SqlParameter("@id", role.Id));
            return Results.Ok(exists);
        }
    }
}
