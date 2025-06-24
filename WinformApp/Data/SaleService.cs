using Alaska.Data;
using Alaska.Models;
using Alaska;
using AlaskaLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinformApp.Data
{
    internal class SaleService : IService
    {
        public Period Period { get; set; } = new Period();
        public async Task<object?> CreateAsync(object model)
        {
            var waiter = (Waiter)model;
            var jsonWaiter = JsonSerializer.Serialize(waiter, AppJsonSerializerContext.Default.Waiter);
            var json = await HttpClientSingleton.PostAsync("/master-data/waiters/", jsonWaiter);
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Waiter) : null;
        }

        public async Task<CommonResult> DeleteAsync(int id)
        {
            var json = await HttpClientSingleton.DeleteAsync("/trans/sales-delete/" + id.ToString());
            var success = json.Trim().ToLower() == "true";
            return new CommonResult() { Success = success };
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/master-data/waiters/" + id.ToString());
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Waiter) : null;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {            
            var json = JsonSerializer.Serialize(Period, AppJsonSerializerContext.Default.Period);
            using (var builder = new SalesTableBuilder(await HttpClientSingleton.PostStreamAsync("/trans/sales", json)))
            {
                return builder.ToDataTable();
            }
        }

        public async Task<CommonResult> UpdateAsync(object model)
        {
            string waiter = JsonSerializer.Serialize((Waiter)model, AppJsonSerializerContext.Default.Waiter);
            string json = await HttpClientSingleton.PutAsync("/master-data/waiters/", waiter);
            CommonResult? result = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult);
            return result != null ? result : new CommonResult() { Success = false };
        }
    }

    public class SalesTableBuilder : DataTableBuilder
    {
        public SalesTableBuilder(Stream stream): base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("date", typeof(DateTime));
            AddColumn("income", typeof(double));
            AddColumn("expense", typeof(double));
            AddColumn("balance", typeof(double));
            AddColumn("notes", typeof(string));
            AddColumn("creator", typeof(string));
            AddColumn("createdDate", typeof(DateTime));

            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(), ReadDateTime(), ReadDouble(), ReadDouble(), 0, ReadString(), ReadString(), ReadDateTime()
                };
                values[4] = (double)values[2] - (double)values[3];
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
