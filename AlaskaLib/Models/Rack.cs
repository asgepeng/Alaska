using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Rack
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
    }
}
