using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public int CustomerId { get; set; } = 0;
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public long Discount { get; set; } = 0;
        public long Tax { get; set; } = 0;
        public long Cost { get; set; } = 0;
        public int SalesPersonId { get; set; } = 0;
        public decimal TotalAmount { get; set; }
        public decimal RoundUp { get; set; } = 0;
        public int ReceiptType { get; set; } = 0;
        public void CalculateTotal(out long totalProductPrice, out long totalProductDiscount, out long totalNettPrice)
        {
            totalProductPrice = 0;
            totalProductDiscount = 0;
            totalNettPrice = 0;
            foreach (var item in this.Items)
            {
                totalProductPrice += item.TotalPrice;
                totalProductDiscount += item.Discount;
            }
            totalNettPrice = totalProductPrice - Discount + Cost;
        }
    }

    public class SaleItem
    {
        [JsonPropertyName("id")] public int ProductId { get; set; }
        [JsonPropertyName("productSku")] public string ProductSku { get; set; } = string.Empty;
        [JsonPropertyName("productName")] public string ProductName { get; set; } = string.Empty;
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 1;
        [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;
        [JsonPropertyName("price")] public long Price { get; set; } = 0;
        [JsonPropertyName("discount")] public long Discount { get; set; } = 0;
        [JsonPropertyName("nettPrice")] public long NettPrice => Price - Discount;
        [JsonPropertyName("totalPrice")] public long TotalPrice => NettPrice * Quantity;
        [JsonPropertyName("productInfo")] public ProductInfo? ProductInfo { get; set; } = null;
    }

    public class ProductInfo
    {
        [JsonPropertyName("basicPrice")] public long BasicPrice { get; set; } = 0;
        [JsonPropertyName("price")] public long Price { get; set; } = 0;
        [JsonPropertyName("wholesalePrice")] public long WholesalePrice { get; set; } = 0;
        [JsonPropertyName("wholesaleQuantity")] public int WholesaleQuantity { get; set; } = 0;
        [JsonPropertyName("availableQuantity")] public int AvailableQuantity { get; set; } = 0;
    }

    public class SaleItemRequest
    {
        [JsonPropertyName("key")] public string Key { get; set; } = "";
    }
}
