using System.Text.Json.Serialization;

namespace Alaska.Models
{
    public class Supplier
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("address")] public string Address { get; set; } = "";
        [JsonPropertyName("phone")] public string Phone { get; set; } = "";
        [JsonPropertyName("email")] public string Email { get; set; } = "";
        [JsonPropertyName("groupId")] public int GroupId { get; set; } = 0;
        [JsonPropertyName("maximumAp")] public long MaximumAP { get; set; } = 0;
        [JsonPropertyName("paymentDeadline")] public int PaymentDeadline { get; set; } = 0;
        public override string ToString()
        {
            return this.Name;
        }
    }

    public class SupplierGroup
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        public override string ToString()
        {
            return Name;
        }
    }

    public class SupplierModel
    {
        [JsonPropertyName("supplier")] public Supplier? Supplier { get; set; } = null;
        [JsonPropertyName("groups")] public List<SupplierGroup> Groups { get; set; } = new List<SupplierGroup>();
    }
}
