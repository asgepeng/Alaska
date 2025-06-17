using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Employee
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("address")] public string Address { get; set; } = "";
        [JsonPropertyName("phone")] public string Phone { get; set; } = "";
        [JsonPropertyName("email")] public string Email { get; set; } = "";
        [JsonPropertyName("departmentId")] public int DepartmentId { get; set; } = 0;
        [JsonPropertyName("roleId")] public int RoleId { get; set; } = 0;
        [JsonPropertyName("upLevelId")] public int UpLevelId { get; set; } = 0;
        [JsonPropertyName("active")] public bool Active { get; set; } = false;
        public override string ToString()
        {
            return Name;
        }
    }

    public class EmployeeModel
    {
        [JsonPropertyName("employee")] public Employee? Employee { get; set; } = null;
        [JsonPropertyName("roles")] public List<Role> Roles { get; set; } = new List<Role>();
        [JsonPropertyName("departments")] public List<Department> Departments { get; set; } = new List<Department>();
        [JsonPropertyName("employess")] public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
