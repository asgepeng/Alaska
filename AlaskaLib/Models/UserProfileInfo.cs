using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class UserProfileInfo
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("address")] public string Address { get; set; } = string.Empty;
        [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
        [JsonPropertyName("phone")] public string Phone { get; set; } = string.Empty;
        [JsonPropertyName("roleName")] public string RoleName { get; set; } = string.Empty;
        [JsonPropertyName("store")] public StoreInfo? Store { get; set; } = null;
    }

    public class StoreInfo
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("address")] public string Address { get; set; } = string.Empty;
        [JsonPropertyName("phone")] public string Phone { get; set; } = string.Empty;
        [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
    }
}
