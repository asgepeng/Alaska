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
        internal static void MapOutletEndPoint(this WebApplication app)
        {
            app.MapGet("/master-data/outlets/", GetAllAsync);
            app.MapGet("/master-data/outlets/{id}", GetByIdAsync);
            app.MapPost("/master-data/outlets/{id}", EditAsync);
            app.MapPut("/master-data/outlets/{id}", EditAsync);
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
        internal static async Task<IResult> GetByIdAsync(HttpContext context, DbClient db, int id)
        {
            return await Task.FromResult(Results.Ok());
        }
        internal static async Task<IResult> CreateAsync(HttpContext context, DbClient db, Outlet model)
        {
            return await Task.FromResult(Results.Ok());
        }
        internal static async Task<IResult> EditAsync(HttpContext context, DbClient db, Outlet model)
        {
            return await Task.FromResult(Results.Ok());
        }
        internal static async Task<IResult> DeleteAsync(HttpContext context, DbClient db, int id)
        {
            return await Task.FromResult(Results.Ok());
        }
    }
}
