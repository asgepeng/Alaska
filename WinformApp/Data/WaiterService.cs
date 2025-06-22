using Alaska;
using Alaska.Data;
using Alaska.Models;
using AlaskaLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WinformApp.Forms;

namespace WinformApp.Data
{
    internal class WaiterService : IService
    {
        public async Task<object?> CreateAsync(object model)
        {
            var waiter = (Waiter)model;
            var jsonWaiter = JsonSerializer.Serialize(waiter, AppJsonSerializerContext.Default.Waiter);
            var json = await HttpClientSingleton.PostAsync("/master-data/waiters/", jsonWaiter);
            return json.Length>0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Waiter) : null;
        }

        public async Task<CommonResult> DeleteAsync(int id)
        {
            var json = await HttpClientSingleton.DeleteAsync("/master-data/waiters/" + id.ToString());
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : new CommonResult() { Success = false };
            return cr is null ? new CommonResult() { Success = false } : cr;
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/master-data/waiters/" + id.ToString());
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Waiter) : null;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {
            using (var builder = new OutletTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/waiters/")))
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

    public class WaiterTableBuilder : DataTableBuilder
    {
        public WaiterTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));
            AddColumn("streetAddress", typeof(string));
            AddColumn("phone", typeof(string));
            AddColumn("createdBy", typeof(string));
            AddColumn("createdDate", typeof(DateTime));

            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(),
                    ReadString(),
                    ReadString(),
                    ReadString(),
                    ReadString(),
                    ReadDateTime()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
