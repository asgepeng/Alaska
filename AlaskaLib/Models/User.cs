using System.Text.Json.Serialization;

namespace Alaska.Models
{
    public class User
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("login")] public string Login { get; set; } = "";
        [JsonPropertyName("password")] public string Password { get; set; } = "";
        [JsonPropertyName("roleId")] public int RoleId { get; set; } = 0;
        [JsonPropertyName("addresses")] public List<Address> Addresses { get; set; } = new List<Address>();
        [JsonPropertyName("phones")] public List<Phone> Phones { get; set; } = new List<Phone>();
        [JsonPropertyName("emails")] public List<Email> Emails { get; set; } = new List<Email>();
    }

    public class UserViewModel
    {
        [JsonConstructor] public UserViewModel() { User = new User(); }
        [JsonPropertyName("user")] public User User { get; set; }
        [JsonPropertyName("roles")] public List<Role> Roles { get; set; } = new List<Role>();
    }

    public class ResetPasswordModel
    {
        [JsonPropertyName("userId")] public int UserID { get; set; } = 0;
        [JsonPropertyName("oldPassword")] public string OldPassword { get; set; } = "";
        [JsonPropertyName("newPassword")] public string NewPassword { get; set; } = "";
    }
}
