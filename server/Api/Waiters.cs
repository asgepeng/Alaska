using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace server.Api
{
    internal static class Waiters
    {
        internal static void MapWaiterEndPoints(this WebApplication app)
        {
            app.MapGet("/master-data/waiters", GetAllAsync).RequireAuthorization();
            app.MapGet("/master-data/waiters/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/master-data/waiters", CreateAsync).RequireAuthorization();
            app.MapPut("/master-data/waiters", UpdateAsync).RequireAuthorization();
            app.MapDelete("/master-data/waiters/{id}", DeleteAsync).RequireAuthorization();
        }
        internal static async Task<IResult> GetAllAsync(DbClient db)
        {
            var commandText = """
                        SELECT w.[id], w.[name], w.[streetAddress], w.[phone], u.[name] AS createdBy, w.createdDate
                        FROM waiters AS w
                        INNER JOIN users AS u ON w.createdBy = u.id
                        WHERE w.deleted = 0
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
                        builder.WriteString(reader.GetString(4));
                        builder.WriteDateTime(reader.GetDateTime(5));
                    }
                    data = builder.ToArray();
                }, commandText);
            }
            return Results.File(data, "application/octet-stream", "waiters.bin");
        }
        internal static async Task<IResult> GetByIdAsync(int id, DbClient db)
        {
            var commandText = """
                SELECT [id], [name], [streetAddress], [phone], [email]
                FROM waiters
                WHERE [id] = @id AND deleted = 0
                """;
            Waiter? model = null;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    model = new Waiter()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        StreetAddress = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Email = reader.GetString(4)
                    };
                }
            }, commandText, new SqlParameter("@id", id));
            if (model != null)
            {
                return Results.Ok(model);
            }
            return Results.NotFound();
        }
        internal static async Task<IResult> CreateAsync(Waiter model, DbClient db, HttpContext context)
        {
            var userID = AppHelpers.GetUserID(context);
            var commandText = """
                INSERT INTO waiters ([name], [streetAddress], [phone], [email], [createdBy])
                VALUES (@name, @streetAddress, @phone, @email, @author);
                SELECT SCOPE_IDENTITY()
                """;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", model.Name),
                new SqlParameter("@streetAddress", model.StreetAddress),
                new SqlParameter("@phone", model.Phone),
                new SqlParameter("@email", model.Email),
                new SqlParameter("@author", userID)
            };
            model.Id = await db.ExecuteScalarIntegerAsync(commandText, parameters);
            
            return Results.Ok(model);
        }
        internal static async Task<IResult> UpdateAsync(Waiter model, DbClient db, HttpContext context)
        {
            var commandText = """
                UPDATE waiters
                SET [name] =@name, [streetAddress]=@streetAddress, [phone]=@phone, [email]=@email
                WHERE id=@id
                """;
            var userID = AppHelpers.GetUserID(context);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", model.Name),
                new SqlParameter("@streetAddress", model.StreetAddress),
                new SqlParameter("@phone", model.Phone),
                new SqlParameter("@email", model.Email),
                new SqlParameter("@author", userID),
                new SqlParameter("@id", model.Id)
            };
            if (await db.ExecuteNonQueryAsync(commandText, parameters))
            {
                return Results.Ok(new CommonResult() { Success = true, Message = "Sukses memperbarui data waiter" });
            }
            return Results.Ok(new CommonResult() { Success = false, Message = "Gagal memperbarui data waiter" });
        }
        internal static async Task<IResult> DeleteAsync(int id, DbClient db, HttpContext context)
        {
            var commandText = """
                UPDATE waiters SET deleted = 1
                WHERE id = @id
                """;
            if (await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id)))
            {
                return Results.Ok(new CommonResult() { Success = true, Message = "Berhasil menghapus record dari database" });
            }
            return Results.Ok(new CommonResult() { Success = false, Message = "Gagal menghapus record dari database" });
        }
    }
}
