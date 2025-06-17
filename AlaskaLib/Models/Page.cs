using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Page
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("title")] public string Title { get; set; } = "";
        [JsonPropertyName("navTitle")] public string NavigationTitle { get; set; } = string.Empty;
        [JsonPropertyName("imageUrl")] public string ImageUrl { get; set; } = string.Empty;
        [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;
        [JsonPropertyName("status")] public int Status { get; set; } = 0;
    }
}
