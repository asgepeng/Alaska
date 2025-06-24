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
            app.MapPost("/reports/cashflows", GetAllAsync).RequireAuthorization();
        }
        internal static async Task<IResult> GetAllAsync(Period period, HttpContext context, DbClient db)
        {
            var commandTex = """
                SELECT c.id, c.[date], c.debt, c.credit, c.notes, u.[name] AS creator
                FROM cashflows AS c
                INNER JOIN users AS u ON c.creator = u.id
                WHERE c.[date] BETWEEN @start AND @end
                ORDER BY c.[date]
                """;
            byte[] data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    double startBalance = 0;
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteDateTime(reader.GetDateTime(1));
                        double debt = reader.GetDouble(2);
                        double credit = reader.GetDouble(3);
                        startBalance += credit - debt;
                        builder.WriteDouble(debt);
                        builder.WriteDouble(credit);
                        builder.WriteDouble(startBalance);
                        builder.WriteString(reader.GetString(4));
                        builder.WriteString(reader.GetString(5));
                    }
                    data = builder.ToArray();
                }, commandTex, new SqlParameter("@start", period.From), new SqlParameter("@end", period.To));
            }
            return Results.File(data, "application/octet-stream");
        }
    }
}
