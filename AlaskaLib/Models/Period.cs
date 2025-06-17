using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Period
    {
        [JsonPropertyName("from")] public DateTime From { get; set; } = DateTime.Today;
        [JsonPropertyName("to")] public DateTime To { get; set; } = DateTime.Now;
    }
}
