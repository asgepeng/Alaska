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
    internal static class ExpenseCategories
    {
        internal static void MapExpenseCategoryEndPoints(this WebApplication app)
        {
            app.MapGet("/master-data/expense-categories", GetAllAsync).RequireAuthorization();
            app.MapGet("/master-data/expense-categories/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/master-data/expense-categories", CreateAsync).RequireAuthorization();
            app.MapPut("/master-data/expense-categories", UpdateAsync).RequireAuthorization();
            app.MapDelete("/master-data/expense-categories/{id}", DeleteAsync).RequireAuthorization();

        }
        internal static async Task<IResult> GetAllAsync(DbClient db)
        {
            var commandText = """
                SELECT ct.id, [ct].[name], CASE WHEN ct.[type] = 0 THEN 'Pemasukan' ELSE 'Pengeluaran' END AS [type], u.[name] AS creator, ct.createdDate
                FROM costTypes AS ct
                INNER JOIN users AS u ON ct.creator = u.id
                WHERE ct.[deleted] = 0
                ORDER BY ct.[id]
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
                        builder.WriteString(reader.GetString(2));
                        builder.WriteString(reader.GetString(3));
                        builder.WriteDateTime(reader.GetDateTime(4));
                    }
                    data = builder.ToArray();
                }, commandText);
            }
            return Results.File(data, "application/octet-stream");
        }
        internal static async Task<IResult> GetByIdAsync(int id, DbClient db)
        {
            var commandText = """
                SELECT id, [name], [type]
                FROM costTypes
                WHERE id=@id
                """;
            CostCategory? model = null;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    model = new CostCategory()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Type = reader.GetInt32(2)
                    };
                }
            }, commandText, new SqlParameter("@id", id));
            return Results.Ok(model);
        }
        internal static async Task<IResult> CreateAsync(CostCategory model, DbClient db, HttpContext context)
        {
            var commandText = """
                INSERT INTO costTypes
                ([name], [type], [creator])
                VALUES 
                (@name, @type, @creator)
                """;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", model.Name),
                new SqlParameter("@type", model.Type),
                new SqlParameter("@creator", AppHelpers.GetUserID(context))
            };
            var success = await db.ExecuteNonQueryAsync(commandText, parameters);
            return Results.Ok(success);
        }
        internal static async Task<IResult> UpdateAsync(CostCategory model, DbClient db, HttpContext context)
        {
            var commandText = """
                UPDATE costTypes
                SET [name]=@name, editor=@editor, editedDate = GETDATE()
                WHERE id=@id
                """;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", model.Name),
                new SqlParameter("@editor", AppHelpers.GetUserID(context)),
                new SqlParameter("@id", model.Id)
            };
            var success = await db.ExecuteNonQueryAsync(commandText, parameters);
            return Results.Ok(success);
        }
        internal static async Task<IResult> DeleteAsync(int id, DbClient db, HttpContext context)
        {
            var commandText = """
                UPDATE costTypes 
                SET deleted = 1, editedDate=GETDATE() 
                WHERE id=@id
                """;
            var success = await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id));
            return Results.Ok(success);
        }
    }
}
