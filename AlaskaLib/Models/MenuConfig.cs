using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    internal class MenuConfig
    {
    }

    public class ApplicationMenuAccess
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("allowAdd")] public bool AllowAdd { get; set; } = false;
        [JsonPropertyName("allowEdit")] public bool AllowEdit { get; set; } = false;
        [JsonPropertyName("allowDelete")] public bool AllowDelete { get; set; } = false;
    }

    public class ApplicationMenuGroup
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("items")] public List<ApplicationMenuAccess> Items { get; set; } = new List<ApplicationMenuAccess>();
    }
}
