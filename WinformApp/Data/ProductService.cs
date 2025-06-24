using Alaska;
using Alaska.Data;
using Alaska.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinformApp.Data
{
    internal class ProductService : IService
    {
        public Period Period { get; set; } = new Period();
        public async Task<object?> CreateAsync(object model)
        {
            var json = await HttpClientSingleton.PostAsync("/master-data/products", JsonSerializer.Serialize((Product)model, AppJsonSerializerContext.Default.Product));
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.Product) : null;
        }

        public async Task<CommonResult> DeleteAsync(int id)
        {
            var json = await HttpClientSingleton.DeleteAsync("/master-data/products/" + id.ToString());
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : null;
            return cr is null ? new CommonResult() { Success = false } : cr;
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/master-data/products/" + id.ToString());
            return json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.ProductViewModel) : null;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {
            using (var builder = new ProductTableBuilder(await HttpClientSingleton.GetStreamAsync("/master-data/products/")))
            {
                return builder.ToDataTable();
            }
        }

        public async Task<CommonResult> UpdateAsync(object model)
        {
            var json = await HttpClientSingleton.PutAsync("/master-data/products", JsonSerializer.Serialize((Product)model, AppJsonSerializerContext.Default.Product));
            var cr = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.CommonResult) : null;
            return cr is null ? new CommonResult() { Success = false } : cr;
        }
    }

    public class ProductTableBuilder : DataTableBuilder
    {
        public ProductTableBuilder(Stream stream) : base(stream)
        {
        }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("sku", typeof(string));
            AddColumn("name", typeof(string));
            AddColumn("category", typeof(string));
            AddColumn("stock", typeof(int));
            AddColumn("unit", typeof(string));
            AddColumn("price", typeof(long));
            AddColumn("creator", typeof(string));
            AddColumn("createdDate", typeof(DateTime));
            AddColumn("lastModified", typeof(DateTime));

            while (Read())
            {
                object[] values = new object[]
                {
                    ReadInt32(),
                    ReadString(),
                    ReadString(),
                    ReadString(),
                    ReadInt32(),
                    ReadString(),
                    ReadDouble(),
                    ReadString(),
                    ReadDateTime(),
                    ReadDateTime()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
