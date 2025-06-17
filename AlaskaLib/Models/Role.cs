using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Role
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("enabled")] public bool Enabled { get; set; } = true;
        public override string ToString()
        {
            return this.Name;
        }
    }
    public class RoleModel
    {
        [JsonPropertyName("role")] public Role? Role { get; set; } = null;
        [JsonPropertyName("applicationAccess")] public List<ApplicationMenuGroup> ApplicationAccesses { get; set; } = new List<ApplicationMenuGroup>();
    }
}
