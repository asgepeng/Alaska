using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class Product
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("sku")] public string SKU { get; set; } = "";
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("description")] public string Description { get; set; } = "";
        [JsonPropertyName("category")] public int Category { get; set; } = 0;
        [JsonPropertyName("productType")] public int ProductType { get; set; } = 0;
        [JsonPropertyName("stock")] public int Stock { get; set; } = 0;
        [JsonPropertyName("unit")] public string Unit { get; set; } = "";
        [JsonPropertyName("images")] public List<string> Images { get; set; } = new List<string>();
        [JsonPropertyName("basicPrice")] public double BasicPrice { get; set; } = 0;
        [JsonPropertyName("price")] public double Price { get; set; } = 0;
        [JsonPropertyName("wholesalePrice")] public long WholesalePrice { get; set; } = 0;
        [JsonPropertyName("wholesaleQuantity")] public int WholesaleQuantity { get; set; } = 0;
        [JsonPropertyName("minStock")] public int MinStock { get; set; } = 0;
        [JsonPropertyName("maxStock")] public int MaxStock { get; set; } = 0;
        [JsonPropertyName("isActive")] public bool IsActive { get; set; } = false;
    }
    public class ProductImage
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("imageUrl")] public string ImageURL { get; set; } = "";
    }
    public class ProductCategory
    {
        [JsonPropertyName("id")] public int Id { get; set; } = 0;
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        public override string ToString()
        {
            return Name;
        }
    }

    public class ProductViewModel
    {
        [JsonPropertyName("product")] public Product Product { get; set; } = new Product();
        [JsonPropertyName("categories")] public List<ProductCategory>? Categories { get; set; } = null;
        [JsonPropertyName("units")] public List<string>? Units { get; set; } = null;
        [JsonPropertyName("stocks")] public List<StockInfo>? Stocks { get; set; } = null;
    }

    public class StockInfo
    {
        [JsonPropertyName("department")] public string Department { get; set; } = "";
        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 0;
    }
}
