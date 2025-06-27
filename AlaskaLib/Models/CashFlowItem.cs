using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlaskaLib.Models
{
    public class CashFlowItem
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("date")] public DateTime Date { get; set;} = DateTime.Now;
        [JsonPropertyName("direction")] public int Direction { get; set; } = 0;
        [JsonPropertyName("category")] public int Category { get; set; } = 0;
        [JsonPropertyName("amount")] public double Amount { get; set; } = 0;
        [JsonPropertyName("notes")] public string Notes { get; set; } = "";
    }
}
