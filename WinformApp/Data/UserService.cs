using Alaska;
using Alaska.Data;
using Alaska.Models;
using System.Data;
using System.Text.Json;

namespace WinformApp.Data
{
    internal interface IService<T> where T : class
    {
        Task<Stream> GetDataDataTable();
        Task<object?> GetByIdAsync(int id);
        Task<T?> CreateAsync(T model);
        Task<CommonResult> UpdateAsync(T model);
        Task<CommonResult> DeleteAsync(int id);
    }
    internal class UserService : IService<User>
    {
        public Task<User?> CreateAsync(User model)
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
            return await HttpClientSingleton.GetStreamAsync("/user-manager/");
        }

        public Task<CommonResult> UpdateAsync(User model)
        {
            throw new NotImplementedException();
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
