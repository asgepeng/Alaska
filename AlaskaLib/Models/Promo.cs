using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Promo
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("startDate")] public DateTime StartDate { get; set; } = DateTime.Now;
        [JsonPropertyName("endDate")] public DateTime EndDate { get; set; } = DateTime.Now;
        [JsonPropertyName("active")] public bool Active { get; set; } = false;
        [JsonPropertyName("type")] public int Type { get; set; } = 0;
    }

    public class PromoModel
    {
        [JsonPropertyName("promo")] public Promo? Promo { get; set; } = null;
    }
}
