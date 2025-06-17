using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Payment
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("paymentType")] public PaymentType? PaymentType { get; set; } = null;
        [JsonPropertyName("amount")] public long Amount { get; set; } = 0;
    }

    public class PaymentType
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
    }
}
