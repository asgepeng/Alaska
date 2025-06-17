using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Customer
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("address")] public string Address { get; set; } = "";
        [JsonPropertyName("phone")] public string Phone { get; set; } = "";
        [JsonPropertyName("email")] public string Email { get; set; } = "";
        [JsonPropertyName("groupId")] public int GroupId { get; set; } = 0;
        [JsonPropertyName("maximumAr")] public long MaximumAR { get; set; } = 0;
        [JsonPropertyName("paymentDeadline")] public int PaymentDeadline { get; set; } = 0;
    }

    public class CustomerGroup
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        public override string ToString()
        {
            return Name;
        }
    }

    public class CustomerModel
    {
        [JsonPropertyName("customer")] public Customer? Customer { get; set; } = null;
        [JsonPropertyName("groups")] public List<CustomerGroup> Groups { get; set; } = new List<CustomerGroup>();
    }
}
