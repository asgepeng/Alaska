using Alaska.Data;
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
            string commandText = "SELECT [id], [name], [location] FROM outlets WHERE deleted = 0";
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Outlet()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }, commandText);
            return Results.Ok(list);
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
