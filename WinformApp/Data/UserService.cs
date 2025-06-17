using Alaska;
using Alaska.Data;
using Alaska.Models;
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
}
