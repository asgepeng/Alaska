using Alaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlaskaLib.Models
{
    public class ExportProfile
    {
        [JsonPropertyName("type")] public int Type { get; set; } = 0;
        [JsonPropertyName("periode")] public Period? Period { get; set; }
    }
}
