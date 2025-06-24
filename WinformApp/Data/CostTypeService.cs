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

namespace WinformApp.Data
{
    internal class CostTypeService : IService
    {
        public Period Period { get; set; } = new Period();
        public async Task<object?> CreateAsync(object model)
        {
            var json = await HttpClientSingleton.PostAsync("/master-data/expense-categories", JsonSerializer.Serialize((CostCategory)model, AppJsonSerializerContext.Default.CostCategory));
            var success = json.Trim() == "true";
            return new CommonResult() { Success = true };
        }

        public async Task<CommonResult> DeleteAsync(int id)
        {
            var json = await HttpClientSingleton.DeleteAsync("/master-data/expense-categories/" + id.ToString());
            var success = json.Trim() == "true";
            return new CommonResult() { Success = success };
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/master-data/expense-categories/" + id.ToString());
            var costType = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CostCategory) : null;
            return costType;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {
            using (var builder = new CostTypeTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/expense-categories")))
            {
                return builder.ToDataTable();
            }
        }

        public async Task<CommonResult> UpdateAsync(object model)
        {
            var jsonObject = JsonSerializer.Serialize((CostCategory)model, AppJsonSerializerContext.Default.CostCategory);
            var json = await HttpClientSingleton.PutAsync("/master-data/expense-categories", jsonObject);
            var success = json.Trim() == "true";
            return new CommonResult() { Success = true };
        }
    }

    public class CostTypeTableBuilder : DataTableBuilder
    {
        public CostTypeTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));
            AddColumn("type", typeof(string));
            AddColumn("creator", typeof(string));
            AddColumn("createdDate", typeof(DateTime));
            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(), ReadString(), ReadString(), ReadString(),ReadDateTime()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
