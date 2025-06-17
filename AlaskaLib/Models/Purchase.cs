using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Purchase
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("invoiceNumber")] public string InvoiceNumber { get; set; } = "";
        [JsonPropertyName("date")] public DateTime Date { get; set; } = DateTime.Now;
        [JsonPropertyName("supplierId")] public int supplierId { get; set; } = 0;
        [JsonPropertyName("purchaseOrder")] public string PurchaseOrder { get; set; } = "";
        [JsonPropertyName("operator")] public string Operator { get; set; } = "";
        [JsonPropertyName("discount")] public long Discount { get; set; } = 0;
        [JsonPropertyName("tax")] public long Tax { get; set; } = 0;
        [JsonPropertyName("cost")] public long Cost { get; set; } = 0;
        [JsonPropertyName("items")] public List<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();

    }
    
    public class PurchaseItem
    {
        [JsonPropertyName("itemId")] public int ItemId { get; set; } = 0;
        [JsonPropertyName("itemName")] public string ItemName { get; set; } = "";
        [JsonPropertyName("basicPrice")] public long BasicPrice { get; set; } = 0;
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 0;
        [JsonPropertyName("unit")] public string Unit { get; set; } = "";
        [JsonPropertyName("price")] public long Price { get; set; } = 0;
        [JsonPropertyName("discount")] public long Discount { get; set; } = 0;
        [JsonPropertyName("nettPrice")] public long NettPrice
        {
            get
            {
                return this.Price - this.Discount;
            }
        }
        [JsonPropertyName("total")] public long Total
        {
            get
            {
                return this.NettPrice * Quantity;
            }
        }
        
    }
}
