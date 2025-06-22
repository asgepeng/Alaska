using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlaskaLib.Models
{
    public class DropdownOption
    {
        [JsonPropertyName("id")]public int Id { get; set; } = 0;
        [JsonPropertyName("text")] public string Text { get; set; } = "";
        public override string ToString()
        {
            return Text;
        }
    }
}
