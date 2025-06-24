using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlaskaLib.Models
{
    public class Outlet
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("address")] public string Address { get; set; } = "";
        [JsonPropertyName("type")] public int Type { get; set; } = 0;
        [JsonPropertyName("waiter")] public int Waiter { get; set; }
    }

    public class OutletViewModel
    {
        [JsonPropertyName("outlet")] public Outlet Outlet { get; set; } = new Outlet();
        [JsonPropertyName("waiters")] public List<DropdownOption> Waiters { get; set; } = new List<DropdownOption>();
    }
}
