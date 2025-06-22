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
    internal static class CashFlows
    {
        internal static void MapCashflowEndPoints(this WebApplication app)
        {
            app.MapGet("/reports/cashflows", GetAllAsync).RequireAuthorization();
        }
        internal static async Task<IResult> GetAllAsync(HttpContext context, DbClient db)
        {
            var commandTex = """
                SELECT c.id, c.[date], c.debt, c.credit, c.notes, u.[name] AS creator
                FROM cashflows AS c
                INNER JOIN users AS u ON c.creator = u.id
                """;
            byte[] data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteDateTime(reader.GetDateTime(1));
                    }
                }, commandTex);
            }
            return Results.File(data, "application/octet-stream");
        }
    }
}
