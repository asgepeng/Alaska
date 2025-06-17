using System.Text.Json.Serialization;

namespace Alaska.Models
{
    public class Login
    {
        [JsonPropertyName("id")] public int Id { get; private set; } = 0;
        [JsonPropertyName("username")] public string Username { get; private set; } = "";
        [JsonPropertyName("actor")] public int Actor { get; private set; } = 0;
    }
}
