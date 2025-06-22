using Alaska.Data;
using Alaska;
using Alaska.Models;
using AlaskaLib.Models;
using System.Data;
using System.Text.Json;

namespace WinformApp.Data
{
    internal class OutletService : IService
    {
        public async Task<object?> CreateAsync(object model)
        {
            Outlet outlet = (Outlet)model;
            var json = await HttpClientSingleton.PostAsync("/master-data/outlets/", JsonSerializer.Serialize(outlet, AppJsonSerializerContext.Default.Outlet));
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Outlet) : null;
        }

        public async Task<CommonResult> DeleteAsync(int id)
        {
            var json = await HttpClientSingleton.DeleteAsync("/master-data/outlets/" + id.ToString());
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : new CommonResult() { Success = false };
            return cr is null ? new CommonResult() { Success = false } : cr;
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/master-data/outlets/" + id.ToString());
            var ovm = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.OutletViewModel) : null;
            return ovm;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {
            using (var builder = new OutletTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/outlets/")))
            {
                return builder.ToDataTable();
            }
        }

        public async Task<CommonResult> UpdateAsync(object model)
        {
            var json = await HttpClientSingleton.PutAsync("/master-data/outlets", JsonSerializer.Serialize((Outlet)model, AppJsonSerializerContext.Default.Outlet));
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : new CommonResult() { Success = false };
            return cr is null? new CommonResult() { Success = false } : cr;
        }
    }

    internal class OutletTableBuilder : DataTableBuilder
    {
        internal OutletTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));
            AddColumn("location", typeof(string));
            AddColumn("waiter", typeof(string));
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
