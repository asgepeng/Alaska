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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace server.Api
{
    internal static class CashFlows
    {
        internal static void MapCashflowEndPoints(this WebApplication app)
        {
            app.MapPost("/reports/cashflows", GetAllAsync).RequireAuthorization();
            app.MapPost("/reports/cashflows/export", DownloadExcelAsync).RequireAuthorization();
        }
        internal static async Task<IResult> DownloadExcelAsync(Period period, DbClient db)
        {
            using var stream = new MemoryStream();
            using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                var columns = ExcelHelper.CreateColumnWidths(15, 12, 12, 12, 30, 15, 20);

                var worksheet = new Worksheet();
                worksheet.Append(columns);  
                worksheet.Append(sheetData);

                worksheetPart.Worksheet = worksheet;

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Penjualan"
                });

                ExcelHelper.AddStyles(workbookPart);

                uint rowIndex = 1;
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ("LAPORAN ARUS KAS", 1, CellValues.String)));
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ($"Periode Awal: {period.From:dd/MM/yyyy}", 0, CellValues.String)));
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ($"Periode Akhir: {period.To:dd/MM/yyyy}", 0, CellValues.String)));
                rowIndex++;

                sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                    ("Tanggal", 3, CellValues.String),
                    ("Kas Keluar", 3, CellValues.String),
                    ("Kas Masuk", 3, CellValues.String),
                    ("Saldo", 3, CellValues.String),
                    ("Keterangan", 3, CellValues.String),
                    ("Dibuat oleh", 3, CellValues.String),
                    ("Dibuat tanggal", 3, CellValues.String)));

                var commandText = """
                    SELECT cf.[date], cf.debt, cf.credit, cf.notes, u.[name] AS creator, cf.createdDate
                    FROM cashflows AS cf
                    INNER JOIN users AS u ON cf.creator = u.id
                    WHERE cf.[date] BETWEEN @start AND @end
                    ORDER BY cf.[date]
                    """;

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@start", period.From),
                    new SqlParameter("@end", period.To)
                };

                double balance = 0, totalIncome = 0, totalExpense = 0;
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        var date = reader.GetDateTime(0);
                        var debt = reader.GetDouble(1);
                        var credit = reader.GetDouble(2);
                        var notes = reader.GetString(3);
                        var creator = reader.GetString(4);
                        var createdDate = reader.GetDateTime(5);
                        balance += (credit - debt);

                        sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                           (date.ToString("dd/MM/yyyy"), 0, CellValues.String),
                           (debt, 8, CellValues.Number),
                           (credit, 8, CellValues.Number),
                           (balance, 8, CellValues.Number),
                           (notes, 0, CellValues.String),
                           (creator, 0, CellValues.String),
                           (createdDate.ToString("dd/MM/yyyy HH:mm"), 0, CellValues.String)));
                        totalIncome += credit;
                        totalExpense += debt;
                    }
                    balance = totalIncome - totalExpense;
                    sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                           ("TOTAL", 10, CellValues.String),
                           (totalExpense, 9, CellValues.Number),
                           (totalIncome, 9, CellValues.Number),
                           (balance, 9, CellValues.Number),
                           ("", 10, CellValues.String),
                           ("", 10, CellValues.String),
                           ("", 10, CellValues.String)));
                }, commandText, parameters);
            }
            var data = stream.ToArray();
            return Results.File(data, "application/octet-stream");
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
