using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Menu
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; } = 0;
    }
    public class MenuGroup
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("items")]
        public List<Menu> Items { get; set; } = new List<Menu>();
    }
}
