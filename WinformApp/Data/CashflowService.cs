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
    internal class CashflowService : IService
    {
        public async Task<object?> CreateAsync(object model)
        {
            Outlet outlet = (Outlet)model;
            var json = await HttpClientSingleton.PostAsync("/master-data/outlets/", JsonSerializer.Serialize(outlet, AppJsonSerializerContext.Default.Outlet));
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Outlet) : null;
        }

        public Task<CommonResult> DeleteAsync(int id)
        {
            MessageBox.Show("Record ini tidak bisa dihapus", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return Task.FromResult(new CommonResult() { Success = false });
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/master-data/outlets/" + id.ToString());
            var ovm = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.OutletViewModel) : null;
            return ovm;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {
            using (var builder = new CashflowTableBuilder(await HttpClientSingleton.GetStreamAsync("/reports/cashflows")))
            {
                return builder.ToDataTable();
            }
        }

        public async Task<CommonResult> UpdateAsync(object model)
        {
            var json = await HttpClientSingleton.PutAsync("/master-data/outlets", JsonSerializer.Serialize((Outlet)model, AppJsonSerializerContext.Default.Outlet));
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : new CommonResult() { Success = false };
            return cr is null ? new CommonResult() { Success = false } : cr;
        }
    }

    public class CashflowTableBuilder : DataTableBuilder
    {
        public CashflowTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("date", typeof(DateTime));
            AddColumn("debt", typeof(double));
            AddColumn("credit", typeof(double));
            AddColumn("balance", typeof(double));
            AddColumn("notes", typeof(string));
            AddColumn("creator", typeof(string));
            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(), ReadDateTime(), ReadDouble(), ReadDouble(), ReadDouble(), ReadString(), ReadString()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
