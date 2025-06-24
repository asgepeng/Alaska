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
    internal static class Categories
    {
        internal static void MapCategoryEndPoints(this WebApplication app)
        {
            app.MapGet("/master-data/categories", GetAllAsync).RequireAuthorization();
            app.MapPost("/master-data/categories", CreateAsync).RequireAuthorization();
            app.MapPut("/master-data/categories", UpdateAsync).RequireAuthorization();
            app.MapDelete("/master-data/categories/{id}", DeleteAsync).RequireAuthorization();
        }
        internal static async Task<IResult> GetAllAsync(DbClient db)
        {
            var commandText = """
                SELECT id, [name] FROM categories
                ORDER BY [name]
                """;
            var data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteString(reader.GetString(1));
                    }
                    data = builder.ToArray();
                }, commandText);
            }
            return Results.File(data, "application/octet-stream");
        }
        internal static async Task<IResult> CreateAsync(ProductCategory model, DbClient db)
        {
            var commandText = "SELECT 1 FROM categories WHERE [name]=@name AND NOT id=@id";
            var anyRecords = await db.AnyRecords(commandText, new SqlParameter("@name", model.Name), new SqlParameter("@id", model.Id));
            if (anyRecords)
            {
                return Results.Ok(new CommonResult() { Success = false, Message = $"'{model.Name}' sudah terdaftar di database, gunakan nama lain" });
            }

            commandText = "INSERT INTO categories ([name]) VALUES (@name);";
            var success = await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@name", model.Name));
            return Results.Ok(new CommonResult() { Success = success, Message = "Kategori produk berhasil ditambahkan" });
        }
        internal static async Task<IResult> UpdateAsync(ProductCategory model, DbClient db)
        {
            var commandText = "SELECT 1 FROM categories WHERE [name]=@name AND NOT id=@id";
            var anyRecords = await db.AnyRecords(commandText, new SqlParameter("@name", model.Name), new SqlParameter("@id", model.Id));
            if (anyRecords)
            {
                return Results.Ok(new CommonResult() { Success = false, Message = $"'{model.Name}' sudah terdaftar di database, gunakan nama lain" });
            }
            commandText = "UPDATE categories SET [name]=@name WHERE id=@id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@id", model.Id),
                new SqlParameter("@name", model.Name)
            };
            var success = await db.ExecuteNonQueryAsync(commandText, parameters);
            return Results.Ok(new CommonResult() { Success = success, Message = "Kategori produk berhasil diubah" });
        }
        internal static async Task<IResult> DeleteAsync(int id, DbClient db)
        {
            var commandText = "SELECT 1 FROM products WHERE category = @category AND deleted = 0";
            var anyRecords = await db.AnyRecords(commandText, new SqlParameter("@category", id));
            if (anyRecords)
            {
                return Results.Ok(new CommonResult() { Success = false, Message = "Tidak bisa menghapus data kategori karena masih digunakan pada data produk" });
            }

            commandText = "DELETE FROM categories WHERE id=@id";
            var success = await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id));
            return Results.Ok(new CommonResult() { Success = true });
        }
    }
}
