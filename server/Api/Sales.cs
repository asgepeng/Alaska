using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Reflection.Metadata.Ecma335;

namespace server.Api
{
    internal static class Sales
    {
        internal static void MapSaleEndPoints(this WebApplication app)
        {
            app.MapPost("/trans/sales", GetDataTableAsync).RequireAuthorization();
            app.MapGet("/trans/sales/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/trans/sales-submit", CreateOrUpdateAsync).RequireAuthorization();
            app.MapDelete("/trans/sales-delete/{id}", DeleteAsync).RequireAuthorization();
            app.MapPost("/trans/sales-check", CheckIfExistAsync).RequireAuthorization();
            app.MapPost("/trans/sales/export", ExportSalesReport).RequireAuthorization();
        }
        internal static async Task<IResult> GetDataTableAsync(Period periode, DbClient db)
        {
            var commandText = """
                SELECT ds.[id], ds.[date], cashIn.credit, cashOut.debt, ds.notes, u.[name] AS creator, ds.createdDate
                FROM dailySales As ds
                INNER JOIN cashflows AS cashIn ON ds.cashIn = cashIn.id
                INNER JOIN cashflows AS cashOut ON ds.cashOut = cashOut.id
                INNER JOIN users AS u ON ds.creator = u.id
                WHERE ds.createdDate BETWEEN @from AND @to
                """;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@from", periode.From),
                new SqlParameter("@to", periode.To)
            };
            var data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteDateTime(reader.GetDateTime(1));
                        builder.WriteDouble(reader.GetDouble(2));
                        builder.WriteDouble(reader.GetDouble(3));
                        builder.WriteString(reader.GetString(4));
                        builder.WriteString(reader.GetString(5));
                        builder.WriteDateTime(reader.GetDateTime(6));
                    }
                    data = builder.ToArray();
                }, commandText, parameters);
            }
            return Results.File(data, "application/octet-stream");
        }
        internal static async Task<IResult> GetByIdAsync(int id, DbClient db)
        {
            var dailySale = new DailySale();
            var commandText = "SELECT id, [date], [cashIn], [cashOut], notes FROM dailySales WHERE id=@id";
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    dailySale.Id = id;
                    dailySale.Date = reader.GetDateTime(1);
                    dailySale.CashinId = reader.GetInt32(2);
                    dailySale.CashoutId = reader.GetInt32(3);
                    dailySale.Notes = reader.GetString(4);
                }
            }, commandText, new SqlParameter("@id", id));
            commandText = """
                SELECT ISNULL(dsi.id, -1) AS id, o.id AS outletId, o.[name] AS outletName, o.waiterId, ISNULL(w.[name], '') AS waiterName, CASE WHEN o.[type] = 0 THEN 'Internal' ELSE 'Mitra' END AS [type], ISNULL(dsi.income, CAST(0 AS FLOAT)) AS income, 
                ISNULL(dsi.expense, CAST(0 AS FLOAT)) AS expense, ISNULL(dsi.notes , '') AS notes
                FROM outlets AS o
                LEFT JOIN waiters AS w ON o.waiterId = w.id
                LEFT JOIN dailySaleItems AS dsi ON o.id = dsi.outlet AND dsi.dailySale = @id
                WHERE o.deleted = 0
                """;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                while (await reader.ReadAsync())
                {
                    var item = new DailySalesItem()
                    {
                        Id = reader.GetInt32(0),
                        OutletId = reader.GetInt32(1),
                        OutletName = reader.GetString(2),
                        WaiterId = reader.GetInt32(3),
                        WaiterName = reader.GetString(4),
                        OutletType = reader.GetString(5),
                        Income = reader.GetDouble(6),
                        Expense = reader.GetDouble(7),
                        Notes = reader.GetString(8)
                    };
                    dailySale.Items.Add(item);
                }
            }, commandText, new SqlParameter("@id", id));
            commandText = """
                SELECT id, [expenseId], [costType], [costAmount]
                FROM dailyExpenses
                WHERE dailySale = @id
                """;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                while (await reader.ReadAsync())
                {
                    var item = new DailyExpenseItem()
                    {
                        Id = reader.GetInt32(0),
                        ExpenseId = reader.GetInt32(1),
                        Amount = reader.GetDouble(2)
                    };
                    dailySale.Expenses.Add(item);
                }
            }, commandText, new SqlParameter("@id", dailySale.Id));
            return Results.Ok(dailySale);
        }
        internal static async Task<IResult> CreateOrUpdateAsync(DailySale model, DbClient db, HttpContext context)
        {
            var (totalIncome, totalExpense) = model.Calculate();
            var userID = AppHelpers.GetUserID(context);
            var commandText = "";
            if (model.Id > 0)
            {
                commandText = """
                    UPDATE dailySales SET notes = @notes WHERE id=@id;
                    UPDATE cashflows SET credit = @credit WHERE id=@cashinId;
                    UPDATE cashflows SET debt = @debt WHERE id=@cashoutId;
                    """;
                foreach (var item in model.Items)
                {
                    if (item.Id > 0)
                    {
                        commandText += "UPDATE dailySaleItems SET income =" + item.Income.ToString("0") + ", expense=" + item.Expense.ToString("0") +
                        ", notes='" + item.Notes.Replace("'", "''") + "' WHERE id=" + item.Id.ToString() + ";\n";
                    }
                    else
                    {
                        commandText += $"""
                            INSERT INTO dailySaleItems ([dailySale], [outlet], [waiter], [income], [expense], [notes])
                            VALUES ({model.Id.ToString()}, {item.OutletId.ToString()}, {item.WaiterId.ToString()}, {item.Income.ToString("0")}, {item.Expense.ToString("0")}, '{item.Notes.Replace("'", "''")}');
                            """;
                    }
                }

                var result = await db.ExecuteNonQueryAsync(commandText, new SqlParameter[]
                {
                    new SqlParameter("@credit", totalIncome),
                    new SqlParameter("@debt", totalExpense),
                    new SqlParameter("@cashinId", model.CashinId),
                    new SqlParameter("@cashoutId", model.CashoutId),
                    new SqlParameter("@notes", model.Notes),
                    new SqlParameter("@id", model.Id)
                });
                return Results.Ok(result);
            }
            else
            {
                commandText = """
                    INSERT INTO dailySales ([date], [notes], [creator])
                    VALUES (@date, @notes, @creator);
                    DECLARE @saleID INT;
                    SET @saleID = SCOPE_IDENTITY();
                    INSERT INTO cashflows ([date], [debt], [credit], [notes], [creator])
                    VALUES (@date, 0, @credit, 'Laporan pemasukan harian', @creator);
                    UPDATE dailySales SET cashIn = (SELECT SCOPE_IDENTITY()) WHERE id=@saleID;
                    INSERT INTO cashflows ([date], [debt], [credit], [notes], [creator])
                    VALUES (@date, @debt, 0, 'Laporan pengeluaran harian', @creator);
                    UPDATE dailySales SET cashOut = (SELECT SCOPE_IDENTITY()) WHERE id=@saleID;
                    INSERT INTO dailySaleItems
                    ([dailySale], [outlet], [waiter], [income], [expense], [notes])
                    VALUES
                    """;
                var rows = new List<string>();
                var cells = new string[6];
                cells[0] = "@saleID";
                foreach (var item in model.Items)
                {
                    cells[1] = item.OutletId.ToString();
                    cells[2] = item.WaiterId.ToString();
                    cells[3] = item.Income.ToString("0");
                    cells[4] = item.Expense.ToString("0");
                    cells[5] = "'" + item.Notes.Replace("'", "''") + "'";
                    rows.Add("(" + string.Join(",", cells) + ")");
                }
                commandText += string.Join(",", rows);

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@date", model.Date),
                    new SqlParameter("@notes", model.Notes),
                    new SqlParameter("@creator", userID),
                    new SqlParameter("@credit", totalIncome),
                    new SqlParameter("@debt", totalExpense)
                };
                var success = await db.ExecuteNonQueryAsync(commandText, parameters);
                return Results.Ok(success);
            }
        }
        internal static async Task<IResult> DeleteAsync(int id, DbClient db, HttpContext context)
        {
            var commandText = """
                SELECT cashIn, cashOut FROM dailySales WHERE id=@id
                """;
            var IDs = new int[2];
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    IDs[0] = reader.GetInt32(0);
                    IDs[1] = reader.GetInt32(1);
                }
            }, commandText, new SqlParameter("@id", id));
            commandText = $@"DELETE FROM cashflows WHERE [id] IN ({string.Join(",", IDs)});
DELETE FROM dailySaleItems WHERE dailySale = @id;
DELETE FROM dailySales WHERE id=@id;";
            return Results.Ok(await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id)));
        }
        internal static async Task<IResult> CheckIfExistAsync(Period period, DbClient db)
        {
            var today = period.From;
            var commandText = "SELECT id FROM dailySales WHERE [date] = @today";
            var id = await db.ExecuteScalarIntegerAsync(commandText, new SqlParameter("@today", today));
            return Results.Ok(id);
        }
        internal static async Task<IResult> ExportSalesReport(Period period, DbClient db)
        {
            using var stream = new MemoryStream();
            using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Laporan"
                });

                ExcelHelper.AddStyles(workbookPart);

                uint rowIndex = 1;
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ("LAPORAN PENJUALAN", 1, CellValues.String)));
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ($"Periode Awal: {period.From:dd/MM/yyyy}", 0, CellValues.String)));
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ($"Periode Akhir: {period.To:dd/MM/yyyy}", 0, CellValues.String)));
                rowIndex++;

                sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                    ("Tanggal", 1, CellValues.String), ("Nama Outlet", 1, CellValues.String),
                    ("Tipe Outlet", 1, CellValues.String), ("Pemasukan", 1, CellValues.String),
                    ("Pengeluaran", 1, CellValues.String), ("Selisih", 1, CellValues.String),
                    ("Keterangan", 1, CellValues.String)));

                double subIncome = 0, subExpense = 0, subBalance = 0;
                double grandIncome = 0, grandExpense = 0, grandBalance = 0;
                int? currentType = null;

                string commandText = """
            SELECT ds.[date], o.[name], CASE WHEN o.[type] = 0 THEN 'Internal' ELSE 'Mitra' END, dsi.income, dsi.expense, dsi.income - dsi.expense, 
            dsi.notes, o.[type] 
            FROM dailySaleItems AS dsi 
            INNER JOIN outlets AS o ON dsi.outlet = o.id
            INNER JOIN dailySales AS ds ON dsi.dailySale = ds.id
            WHERE ds.[date] BETWEEN @from AND @to AND o.deleted = 0
            ORDER BY o.[type], ds.[date]
            """;

                SqlParameter[] parameters = new[]
                {
            new SqlParameter("@from",  period.From),
            new SqlParameter("@to", period.To )
        };

                await db.ExecuteReaderAsync(async reader =>
                {
                    while (await reader.ReadAsync())
                    {
                        var date = reader.GetDateTime(0);
                        var outletName = reader.GetString(1);
                        var outletTypeStr = reader.GetString(2);
                        var income = reader.GetDouble(3);
                        var expense = reader.GetDouble(4);
                        var balance = reader.GetDouble(5);
                        var notes = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        var outletType = reader.GetInt32(7);

                        if (currentType != outletType)
                        {
                            if (currentType != null)
                            {
                                sheetData.Append(ExcelHelper.CreateSubtotalRow("SUBTOTAL INTERNAL", rowIndex++, subIncome, subExpense, subBalance));
                                subIncome = subExpense = subBalance = 0;
                            }
                            currentType = outletType;
                        }

                        sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                            (date.ToString("dd/MM/yyyy"), 0, CellValues.String),
                            (outletName, 0, CellValues.String),
                            (outletTypeStr, 0, CellValues.String),
                            (income, 4, CellValues.Number),
                            (expense, 4, CellValues.Number),
                            (balance, 4, CellValues.Number),
                            (notes, 0, CellValues.String)));

                        subIncome += income;
                        subExpense += expense;
                        subBalance += balance;

                        grandIncome += income;
                        grandExpense += expense;
                        grandBalance += balance;
                    }

                    if (currentType != null)
                    {
                        sheetData.Append(ExcelHelper.CreateSubtotalRow("SUBTOTAL MITRA", rowIndex++, subIncome, subExpense, subBalance));
                    }

                    rowIndex++;
                    sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                        ("GRAND TOTAL", 3, CellValues.String),
                        ("", 3, CellValues.String),
                        ("", 3, CellValues.String),
                        (grandIncome, 7, CellValues.Number),
                        (grandExpense, 7, CellValues.Number),
                        (grandBalance, 7, CellValues.Number),
                        ("", 3, CellValues.String)));
                }, commandText, parameters);
            }

            stream.Position = 0;
            var data = stream.ToArray();

            return Results.File(
                data,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            );
        }
    }
}
