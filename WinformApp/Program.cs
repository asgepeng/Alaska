using System.Text.Json.Serialization;
using Alaska.Models;
using AlaskaLib.Models;
using WinformApp;

namespace Alaska
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }

    [JsonSerializable(typeof(OutletViewModel))]
    [JsonSerializable(typeof(Outlet))]
    [JsonSerializable(typeof(DropdownOption))]
    [JsonSerializable(typeof(Waiter))]
    [JsonSerializable(typeof(UserViewModel))]
    [JsonSerializable(typeof(Record))]
    [JsonSerializable(typeof(LoginRequest))]
    [JsonSerializable(typeof(LoginResponse))]
    [JsonSerializable(typeof(Product))]
    [JsonSerializable(typeof(List<Product>))]
    [JsonSerializable(typeof(ProductCategory))]
    [JsonSerializable(typeof(List<ProductCategory>))]
    [JsonSerializable(typeof(StockInfo))]
    [JsonSerializable(typeof(ProductViewModel))]
    [JsonSerializable(typeof(Customer))]
    [JsonSerializable(typeof(List<CustomerModel>))]
    [JsonSerializable(typeof(List<CustomerGroup>))]
    [JsonSerializable(typeof(Menu))]
    [JsonSerializable(typeof(MenuGroup))]
    [JsonSerializable(typeof(List<Menu>))]
    [JsonSerializable(typeof(List<MenuGroup>))]
    [JsonSerializable(typeof(Bank))]
    [JsonSerializable(typeof(Account))]
    [JsonSerializable(typeof(List<Bank>))]
    [JsonSerializable(typeof(AccountModel))]
    [JsonSerializable(typeof(Supplier))]
    [JsonSerializable(typeof(SupplierModel))]
    [JsonSerializable(typeof(SupplierGroup))]
    [JsonSerializable(typeof(List<SupplierGroup>))]
    [JsonSerializable(typeof(CommonResult))]
    [JsonSerializable(typeof(Department))]
    [JsonSerializable(typeof(List<Department>))]
    [JsonSerializable(typeof(Role))]
    [JsonSerializable(typeof(RoleModel))]
    [JsonSerializable(typeof(List<Role>))]
    [JsonSerializable(typeof(Employee))]
    [JsonSerializable(typeof(List<Employee>))]
    [JsonSerializable(typeof(EmployeeModel))]
    [JsonSerializable(typeof(Promo))]
    [JsonSerializable(typeof(Login))]
    [JsonSerializable(typeof(LoginModel))]
    [JsonSerializable(typeof(RNEType))]
    [JsonSerializable(typeof(Page))]
    [JsonSerializable(typeof(Period))]
    [JsonSerializable(typeof(ApplicationMenuAccess))]
    [JsonSerializable(typeof(ApplicationMenuGroup))]
    [JsonSerializable(typeof(User))]
    [JsonSerializable(typeof(UserProfileInfo))]
    [JsonSerializable(typeof(StoreInfo))]
    [JsonSerializable(typeof(Sale))]
    [JsonSerializable(typeof(SaleItem))]
    [JsonSerializable(typeof(ProductInfo))]
    [JsonSerializable(typeof(Sale))]
    [JsonSerializable(typeof(SaleItem))]
    [JsonSerializable(typeof(SaleItemRequest))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}