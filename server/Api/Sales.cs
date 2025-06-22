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
    internal static class Sales
    {
        internal static void MapSaleEndPoints(this WebApplication app)
        {
            app.MapPost("/trans/sales", GetDataTableAsync).RequireAuthorization();
            app.MapGet("/trans/sales/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/trans/sales-submit", CreateOrUpdateAsync).RequireAuthorization();
            app.MapDelete("/trans/sales-delete/{id}", DeleteAsync).RequireAuthorization();
            app.MapGet("/trans/sales-check", CheckIfExistAsync).RequireAuthorization();
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
                SELECT ISNULL(dsi.id, -1) AS id, o.id AS outletId, o.[name] AS outletName, o.waiterId, w.[name] AS waiterName, ISNULL(dsi.income, CAST(0 AS FLOAT)) AS income, 
                ISNULL(dsi.expense, CAST(0 AS FLOAT)) AS expense, ISNULL(dsi.notes , '') AS notes
                FROM outlets AS o
                INNER JOIN waiters AS w ON o.waiterId = w.id
                LEFT JOIN dailySaleItems AS dsi ON o.id = dsi.outlet AND dsi.dailySale = @id
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
                        Income = reader.GetDouble(5),
                        Expense = reader.GetDouble(6),
                        Notes = reader.GetString(7)
                    };
                    dailySale.Items.Add(item);
                }
            }, commandText, new SqlParameter("@id", id));
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
                    commandText += "UPDATE dailySaleItems SET income =" + item.Income.ToString("0") + ", expense=" + item.Expense.ToString("0") + 
                        ", notes='" + item.Notes.Replace("'", "''") + "' WHERE id=" + item.Id.ToString() + ";\n";
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
        internal static async Task<IResult> CheckIfExistAsync(DbClient db)
        {
            var today = DateTime.Today;
            var commandText = "SELECT id FROM dailySales WHERE [date] = @today";
            var id = await db.ExecuteScalarIntegerAsync(commandText, new SqlParameter("@today", today));
            return Results.Ok(id);
        }
    }
}
