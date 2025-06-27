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
            app.MapGet("/trans/sales/expense-categories", GetExpenseCategories).RequireAuthorization();
            app.MapGet("/trans/sales/income-categories", GetIncomeCategories).RequireAuthorization();
        }
        internal static async Task<IResult> GetDataTableAsync(Period periode, DbClient db)
        {
            var commandText = """
                SELECT ds.[id], ds.[date], ISNULL(cashIn.credit, CAST(0 AS FLOAT)) AS credit, ISNULL(cashOut.debt, CAST(0 AS FLOAT)) AS debt, ds.notes, u.[name] AS creator, ds.createdDate
                FROM dailySales As ds
                LEFT JOIN cashflows AS cashIn ON ds.cashIn = cashIn.id
                LEFT JOIN cashflows AS cashOut ON ds.cashOut = cashOut.id
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
                SELECT id, [costType], [costAmount]
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
                if (totalIncome == 0 && model.CashinId > 0)
                {
                    await db.ExecuteNonQueryAsync("DELETE FROM cashflows WHERE id=@id;UPDATE dailySales SET cashIn = 0 WHERE id=@saleID", new SqlParameter("@id", model.CashinId), new SqlParameter("@saleID", model.Id));
                }
                else
                {
                    if (totalIncome > 0 && model.CashinId == 0)
                    {
                        await db.ExecuteScalarIntegerAsync("INSERT INTO cashflows ([date], [debt], [credit], [notes], [creator]) VALUES (@date, 0, @credit, 'Penjualan', @creator); UPDATE dailySales SET cashIn = (SELECT SCOPE_IDENTITY()) WHERE id=@id;",
                        new SqlParameter[]
                        {
                            new SqlParameter("@date", model.Date),
                            new SqlParameter("@credit", totalIncome),
                            new SqlParameter("@creator", userID),
                            new SqlParameter("@id", model.Id)
                        });
                    }
                    else
                    {
                        await db.ExecuteNonQueryAsync("UPDATE cashflows SET credit = @value WHERE id=@id", new SqlParameter("@id", model.CashinId), new SqlParameter("@value", totalIncome));
                    }
                }
                if (totalExpense == 0 && model.CashoutId > 0)
                {
                    await db.ExecuteNonQueryAsync("DELETE FROM cashflows WHERE id=@id;UPDATE dailySales SET cashOut = 0 WHERE id=@saleID;", new SqlParameter("@id", model.CashoutId), new SqlParameter("@saleID", model.Id));
                }
                else
                {
                    if (totalExpense > 0 && model.CashoutId == 0)
                    {
                        await db.ExecuteScalarIntegerAsync("INSERT INTO cashflows ([date], [debt], [credit], [notes], [creator]) VALUES (@date, @debt, 0, 'Biaya Operasional', @creator); UPDATE dailySales SET cashOut = (SELECT SCOPE_IDENTITY()) WHERE id=@id;",
                        new SqlParameter[]
                        {
                            new SqlParameter("@date", model.Date.AddSeconds(1)),
                            new SqlParameter("@debt", totalExpense),
                            new SqlParameter("@creator", userID),
                            new SqlParameter("@id", model.Id)
                        });
                    }
                    else
                    {
                        await db.ExecuteNonQueryAsync("UPDATE cashflows SET debt = @value WHERE id=@id", new SqlParameter("@id", model.CashoutId), new SqlParameter("@value", totalExpense));
                    }
                }
                commandText = "UPDATE dailySales SET notes = @notes WHERE id=@id; ";
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
                commandText += "DELETE FROM dailyExpenses WHERE dailySale = @id;";
                if (model.Expenses.Count > 0)
                {
                    commandText += """
                        INSERT INTO dailyExpenses (dailySale, costType, costAmount)
                        VALUES
                        """;
                    var rows = new List<string>();
                    foreach (DailyExpenseItem item in model.Expenses)
                    {
                        var cells = new string[3];
                        cells[0] = "@id";
                        cells[1] = item.ExpenseId.ToString();
                        cells[2] = item.Amount.ToString("0");
                        rows.Add("(" + string.Join(",", cells) + ")");
                    }
                    commandText += string.Join(",", rows);
                }

                var result = await db.ExecuteNonQueryAsync(commandText, new SqlParameter[]
                {
                    new SqlParameter("@notes", model.Notes),
                    new SqlParameter("@id", model.Id)
                });
                return Results.Ok(result);
            }
            else
            {
                int cashinId = 0, cashoutid = 0;
                if (totalIncome > 0)
                {
                    cashinId = await db.ExecuteScalarIntegerAsync("INSERT INTO cashflows ([date], [debt], [credit], [notes], [creator]) VALUES (@date, 0, @credit, 'Penjualan', @creator); SELECT SCOPE_IDENTITY()", 
                    new SqlParameter[]
                    {
                        new SqlParameter("@date", model.Date),
                        new SqlParameter("@credit", totalIncome),
                        new SqlParameter("@creator", userID)
                    });
                }
                if (totalExpense > 0)
                {
                    cashoutid = await db.ExecuteScalarIntegerAsync("INSERT INTO cashflows ([date], [debt], [credit], [notes], [creator]) VALUES (@date, @debt, 0, 'Biaya Operasional', @creator); SELECT SCOPE_IDENTITY()",
                    new SqlParameter[]
                    {
                        new SqlParameter("@date", model.Date.AddSeconds(1)),
                        new SqlParameter("@debt", totalExpense),
                        new SqlParameter("@creator", userID)
                    });
                }
                commandText = """
                    INSERT INTO dailySales ([date], [notes], [cashIn], [cashOut], [creator])
                    VALUES (@date, @notes, @cashin, @cashout, @creator);
                    DECLARE @saleID INT;
                    SET @saleID = SCOPE_IDENTITY();
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
                commandText += string.Join(",", rows) + "; ";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@date", model.Date),
                    new SqlParameter("@notes", model.Notes),
                    new SqlParameter("@creator", userID),
                    new SqlParameter("@cashin", cashinId),
                    new SqlParameter("@cashout", cashoutid)
                };
                rows.Clear();
                if (model.Expenses.Count > 0)
                {
                    commandText += "INSERT INTO dailyExpenses ([dailySale], [costType], [costAmount]) VALUES ";
                    var excells = new string[3];
                    excells[0] = "@saleID";
                    foreach (var item in model.Expenses)
                    {
                        excells[1] = item.ExpenseId.ToString();
                        excells[2] = item.Amount.ToString("0");
                        rows.Add("(" + string.Join(",", excells) + ")");
                    }
                    commandText += string.Join(",", rows);
                }
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
                var columns = ExcelHelper.CreateColumnWidths(15, 20, 20, 12, 12, 12, 30);

                var worksheet = new Worksheet();
                worksheet.Append(columns);     // kolom dulu
                worksheet.Append(sheetData);   // baru data

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
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ("LAPORAN PENJUALAN", 1, CellValues.String)));
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ($"Periode Awal: {period.From:dd/MM/yyyy}", 0, CellValues.String)));
                sheetData.Append(ExcelHelper.CreateRow(rowIndex++, ($"Periode Akhir: {period.To:dd/MM/yyyy}", 0, CellValues.String)));
                rowIndex++;

                sheetData.Append(ExcelHelper.CreateRow(rowIndex++,
                    ("Tanggal", 3, CellValues.String),
                    ("Nama Outlet", 3, CellValues.String),
                    ("Tipe Outlet", 3, CellValues.String),
                    ("Pemasukan", 3, CellValues.String),
                    ("Pengeluaran", 3, CellValues.String),
                    ("Selisih", 3, CellValues.String),
                    ("Keterangan", 3, CellValues.String)));

                double subIncome = 0, subExpense = 0, subBalance = 0;
                double grandIncome = 0, grandExpense = 0, grandBalance = 0;
                int? currentType = null;

                string commandText = """
                    SELECT ds.[date], o.[name], CASE WHEN o.[type] = 0 THEN 'Internal' ELSE 'Mitra' END AS outletType,
                    dsi.income, ISNULL(cf.debt, 0) AS expense, dsi.income - ISNULL(cf.debt, 0) AS balance, dsi.notes, o.[type]
                    FROM dailySaleItems AS dsi 
                    INNER JOIN outlets AS o ON dsi.outlet = o.id
                    INNER JOIN dailySales AS ds ON dsi.dailySale = ds.id
                    LEFT JOIN cashflows AS cf ON ds.cashOut = cf.id AND o.[type] = 0
                    WHERE  o.deleted = 0 AND ds.[date] BETWEEN @from AND @to
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
                        var notes = reader.GetString(6);
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
                            (income, 8, CellValues.Number),
                            (expense, 8, CellValues.Number),
                            (balance, 8, CellValues.Number),
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
                        ("GRAND TOTAL", 10, CellValues.String),
                        ("", 10, CellValues.String),
                        ("", 10, CellValues.String),
                        (grandIncome, 9, CellValues.Number),
                        (grandExpense, 9, CellValues.Number),
                        (grandBalance, 9, CellValues.Number),
                        ("", 10, CellValues.String)));
                }, commandText, parameters);

                rowIndex++;

                var worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData2 = new SheetData();
                worksheetPart2.Worksheet = new Worksheet(sheetData2);

                sheets.Append(new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart2),
                    SheetId = 2,
                    Name = "Pengeluaran"
                });
                uint rowIndex2 = 1;
                sheetData2.Append(ExcelHelper.CreateRow(rowIndex2++,
                    ("Tanggal", 3, CellValues.String),
                    ("Kategori", 3, CellValues.String),
                    ("Nilai", 3, CellValues.String)));

                commandText = """
                    SELECT ds.[date], ct.[name] AS [Kategori], de.costAmount AS amount
                    FROM dailyExpenses AS de 
                    INNER JOIN costTypes AS ct ON de.costType = ct.id
                    INNER JOIN dailySales AS ds ON de.dailySale = ds.id
                    WHERE ds.[date] BETWEEN @start AND @end
                    ORDER BY ds.[date]
                    """;
                var pParam = new SqlParameter[]
                {
                    new SqlParameter("@start", period.From),
                    new SqlParameter("@end", period.To)
                };
                double eTotal = 0;
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {                    
                    while (await reader.ReadAsync())
                    {
                        var edate = reader.GetDateTime(0);
                        var eName = reader.GetString(1);
                        var eAmount = reader.GetDouble(2);
                        sheetData2.Append(ExcelHelper.CreateRow(rowIndex2++,
                            (edate.ToString("dd/MM/yyyy"), 0, CellValues.String),
                            (eName, 0, CellValues.String),
                            (eAmount, 4, CellValues.Number)));
                        eTotal += eAmount;
                    }
                    sheetData2.Append(ExcelHelper.CreateRow(rowIndex2++,
                            ("Total Pengeluaran", 3, CellValues.String),
                            ("", 3, CellValues.String),
                            (eTotal, 7, CellValues.Number)));

                }, commandText, pParam);
            }


            stream.Position = 0;
            var data = stream.ToArray();

            return Results.File(
                data,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            );
        }
        internal static async Task<IResult> GetExpenseCategories(DbClient db)
        {
            var data = Array.Empty<byte>();
            using (BinaryBuilder builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteString(reader.GetString(1));
                    }
                    data = builder.ToArray();
                }, "SELECT id, [name] FROM costTypes WHERE [type]=1 AND deleted = 0");
            }
            return Results.File(data, "application/octet-stream");
        }
        internal static async Task<IResult> GetIncomeCategories(DbClient db)
        {
            var data = Array.Empty<byte>();
            using (BinaryBuilder builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));
                        builder.WriteString(reader.GetString(1));
                    }
                    data = builder.ToArray();
                }, "SELECT id, [name] FROM costTypes WHERE [type]=0 AND deleted = 0");
            }
            return Results.File(data, "application/octet-stream");
        }
    }
}
