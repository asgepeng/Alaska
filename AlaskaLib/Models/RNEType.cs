using System.Text.Json.Serialization;

namespace Alaska.Models
{
    public class RNEType
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("type")] public int Type { get; set; } = 0;
        public override string ToString()
        {
            return Name;
        }
    }
}
