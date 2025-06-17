using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Account
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("number")] public string Number { get; set; } = "";
        [JsonPropertyName("bank")] public int Bank { get; set; } = 0;

    }
    public class Bank
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("type")] public int Type { get; set; } = 0;
        public override string ToString()
        {
            return Name;
        }
    }

    public class AccountModel
    {
        [JsonPropertyName("account")] public Account? Account { get; set; } = null;
        [JsonPropertyName("banks")] public List<Bank>? Banks { get; set; } = null;
    }
}
