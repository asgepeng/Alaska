using Alaska;
using Alaska.Data;
using Alaska.Models;
using System.Data;
using System.Text.Json;

namespace WinformApp.Data
{
    internal interface IService
    {
        Task<DataTable> GetDataDataTableAsync();
        Task<object?> GetByIdAsync(int id);
        Task<object?> CreateAsync(object model);
        Task<CommonResult> UpdateAsync(object model);
        Task<CommonResult> DeleteAsync(int id);
    }
    internal class UserService
    {
        public async Task<bool> CreateAsync(User model)
        {
            var json = await HttpClientSingleton.PostAsync("/user-manager", JsonSerializer.Serialize(model, AppJsonSerializerContext.Default.User));
            MessageBox.Show(json);
            return json.Trim() == "true";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var json = await HttpClientSingleton.DeleteAsync("/user-manager/" + id.ToString());
            return json.Trim() == "true";
        }

        public async Task<object?> GetByIdAsync(int id)
        {
            var json = await HttpClientSingleton.GetAsync("/user-manager/" + id.ToString());
            var user = json.Length > 0 ? JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.UserViewModel) : null;
            return user;
        }

        public async Task<DataTable> GetDataDataTableAsync()
        {
            using (var builder = new UserTableBuilder(await HttpClientSingleton.GetStreamAsync("/user-manager/")))
            {
                return builder.ToDataTable();
            }
        }
        public async Task<bool> UpdateAsync(User model)
        {
            var json = await HttpClientSingleton.PutAsync("/user-manager", JsonSerializer.Serialize(model, AppJsonSerializerContext.Default.User));
            return json.Trim() == "true";
        }
    }

    internal class UserTableBuilder : DataTableBuilder
    {
        internal UserTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));
            AddColumn("role", typeof(string));
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
                    ReadDateTime()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
