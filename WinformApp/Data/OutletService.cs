using Alaska.Data;
using Alaska;
using Alaska.Models;
using AlaskaLib.Models;
using System.Data;
using System.Text.Json;

namespace WinformApp.Data
{
    internal class OutletService : IService<Outlet>
    {
        public Task<Outlet?> CreateAsync(Outlet model)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.Instance.GetStringAsync("/user-manager/" + id.ToString());
            var user = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.UserViewModel) : null;
            return user;
        }

        public async Task<Stream> GetDataDataTable()
        {
            return await HttpClientSingleton.GetStreamAsync("/master-data/outlets/");
        }

        public Task<CommonResult> UpdateAsync(Outlet model)
        {
            throw new NotImplementedException();
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
