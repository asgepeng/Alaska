using Alaska.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Api
{
    internal static class Downloads
    {
        internal static void MapExportEndPoints(this WebApplication app)
        {
            app.MapPost("/reports", ExportExcelAsync).RequireAuthorization();
        }
        internal static async Task<IResult> ExportExcelAsync(DbClient db)
        {
            var commandText = """
                SELECT [date], [debt], [credit], [notes], [creator], [createdDate] FROM cashflows
                """;
            var data = await db.ExportToExcelAsync(commandText);
            return Results.File(data, "application/excel");
        }
    }
}
