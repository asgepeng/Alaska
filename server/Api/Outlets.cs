using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
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
    internal static class Outlets
    {
        internal static void MapOutletEndPoints(this WebApplication app)
        {
            app.MapGet("/master-data/outlets", GetAllAsync);
            app.MapGet("/master-data/outlets/{id}", GetByIdAsync);
            app.MapPost("/master-data/outlets", CreateAsync);
            app.MapPut("/master-data/outlets", EditAsync);
            app.MapDelete("/master-data/outlets/{id}", DeleteAsync);
        }
        internal static async Task<IResult> GetAllAsync(HttpContext context, DbClient db)
        {
            var list = new List<Outlet>();
            string commandText = """
                SELECT o.id, o.[name], o.[streetAddress] AS [location], w.[name] AS waiter, u.[name] AS createdBy, o.[createdDate]
                FROM outlets AS o
                INNER JOIN waiters AS w ON o.waiterId = w.id
                INNER JOIN users AS u ON w.createdBy = u.id
                WHERE o.deleted = 0
                """;
            byte[] data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    if (reader != null)
                    {
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
                    }
                }, commandText);
            }
            return Results.File(data, "application/octet-stream");
        }
        internal static async Task<IResult> GetByIdAsync(int id, HttpContext context, DbClient db)
        {
            var model = new OutletViewModel();
            var commandText = "SELECT [id], [name] FROM waiters WHERE deleted = 0";
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        model.Waiters.Add(new DropdownOption()
                        {
                            Id = reader.GetInt32(0),
                            Text = reader.GetString(1)
                        });
                    }
                }
            }, commandText);
            commandText = """
                SELECT [id], [name], [streetAddress], [waiterId]
                FROM outlets
                WHERE deleted = 0 AND id=@id
                """;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    model.Outlet = new Outlet()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Waiter = reader.GetInt32(3)
                    };
                }
            }, commandText, new SqlParameter("@id", id));
            return Results.Ok(model);
        }
        internal static async Task<IResult> CreateAsync(Outlet model, HttpContext context, DbClient db)
        {
            var commandText = """
                INSERT INTO outlets ([name], [streetAddress], [waiterId], [createdBy])
                VALUES (@name, @location, @waiter, @createdBy);
                SELECT SCOPE_IDENTITY()
                """;
            var id = AppHelpers.GetUserID(context);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", model.Name),
                new SqlParameter("@location", model.Address),
                new SqlParameter("@waiter", model.Waiter),
                new SqlParameter("@createdBy", id)
            };
            model.Id = await db.ExecuteScalarIntegerAsync(commandText, parameters);
            return Results.Ok(model);
        }
        internal static async Task<IResult> EditAsync(HttpContext context, DbClient db, Outlet model)
        {
            var commandText = """
                UPDATE outlets
                SET [name]=@name, [streetAddress]=@location, [waiterId]=@waiter, editedDate=GETDATE()
                WHERE id=@id
                """;
            var id = AppHelpers.GetUserID(context);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", model.Name),
                new SqlParameter("@location", model.Address),
                new SqlParameter("@waiter", model.Waiter),
                new SqlParameter("@editedBy", id),
                new SqlParameter("@id", model.Id)
            };
            var result = await db.ExecuteNonQueryAsync(commandText, parameters);
            if (result)
            {
                return Results.Ok(new CommonResult() { Success = true, Message = "Sukses memperbarui data outlet" });
            }
            else
            {
                return Results.Ok(new CommonResult() { Success = false, Message = "Gagal memperbarui data outlet" });
            }
        }
        internal static async Task<IResult> DeleteAsync(HttpContext context, DbClient db, int id)
        {
            var commandText = "UPDATE outlets SET deleted = 1 WHERE id=@id";
            if (await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id)))
            {
                return Results.Ok(new CommonResult() { Success = true, Message = "Data outlet berhasil dihapus dari database" });
            }
            return Results.Ok(new CommonResult() { Success = false });
        }
    }
}
