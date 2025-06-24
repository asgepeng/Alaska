using System.Data;
using System.Text;
using Alaska.Data;
using Alaska.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace server.Api
{
    internal static class Products
    {
        public static void MapProductEndPoints(this WebApplication app)
        {
            app.MapGet("/master-data/products", GetDataTableAsync).RequireAuthorization();
            app.MapGet("/master-data/products/{id}", GetByIdAsync).RequireAuthorization();
            app.MapPost("/master-data/products", CreateAsync).RequireAuthorization();
            app.MapPut("/master-data/products", UpdateAsync).RequireAuthorization();
            app.MapDelete("/master-data/products/{id}", DeleteAsync).RequireAuthorization();
            app.MapGet("/master-data/reset", ResetDataAsync).RequireAuthorization();
        }
        private static async Task<IResult> ResetDataAsync(DbClient db)
        {
            var commandText = """
                TRUNCATE TABLE roles;
                TRUNCATE TABLE users;
                TRUNCATE TABLE authentications;
                TRUNCATE TABLE waiters;
                TRUNCATE TABLE outlets;
                TRUNCATE TABLE categories;
                TRUNCATE TABLE products;
                TRUNCATE TABLE cashflows;
                TRUNCATE TABLE dailyreports;
                TRUNCATE TABLE dailySaleItems;
                TRUNCATE TABLE dailySales;
                INSERT INTO users ([name], [login], [password], [createdBy], [createdAt])
                VALUES ('Administrator', 'admin', HASHBYTES('SHA2_256', CAST('Power123...' AS nvarchar)), 0, GETDATE());
                """;
            var result = await db.ExecuteNonQueryAsync(commandText);
            return Results.Ok(result);
        }
        private static async Task<IResult> GetDataTableAsync(DbClient db)
        {
            string sql = """
                SELECT i.id, i.sku, i.name, c.name AS category, i.stock, i.unit, i.price, ISNULL(u.name, '') AS creator, i.createdDate, i.editedDate
                FROM products AS i
                INNER JOIN categories AS c ON i.category = c.id
                INNER JOIN users AS u ON i.createdBy = u.id
                WHERE i.deleted = 0
                """;
            byte[] data = Array.Empty<byte>();
            using (var builder = new BinaryBuilder())
            {
                await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
                {
                    while (await reader.ReadAsync())
                    {
                        builder.WriteInt32(reader.GetInt32(0));             // id
                        builder.WriteString(reader.GetString(1));           // sku
                        builder.WriteString(reader.GetString(2));           // name
                        builder.WriteString(reader.GetString(3));           // category
                        builder.WriteInt32(reader.GetInt32(4));             // stock
                        builder.WriteString(reader.GetString(5));           // unit
                        builder.WriteDouble(reader.GetDouble(6));            // price
                        builder.WriteString(reader.GetString(7));           // creator
                        builder.WriteDateTime(reader.GetDateTime(8));       // created date
                        if (reader.IsDBNull(9)) builder.WriteInt64((long)0);      // dbnull value
                        else builder.WriteDateTime(reader.GetDateTime(9));  // edited date
                    }
                    data = builder.ToArray();
                }, sql);
            }
            return Results.File(data, "application/octet-stream");
        }
        private static async Task<IResult> GetByIdAsync(int id, DbClient db)
        {
            ProductViewModel model = new ProductViewModel();
            model.Product = new Product();
            model.Categories = new List<ProductCategory>();
            model.Units = new List<string>();
            model.Stocks = new List<StockInfo>();

            string sql = """
                SELECT id, sku, [name], [description], stock, unit, category, costAverage, price, minstock, maxstock, images, isActive
                FROM products WITH (NOLOCK)
                WHERE id=@id AND deleted = 0
                """;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    model.Product = new Product()
                    {
                        Id = reader.GetInt32("id"),
                        SKU = reader.GetString("sku"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("description"),
                        Stock = reader.GetInt32("stock"),
                        Unit = reader.GetString("unit"),
                        Category = reader.GetInt32("category"),
                        BasicPrice = reader.GetDouble("costAverage"),
                        Price = reader.GetDouble("price"),
                        MinStock = reader.GetInt32("minstock"),
                        MaxStock = reader.GetInt32("maxstock"),
                        IsActive = reader.GetBoolean("isActive")
                    };
                    string imageStr = reader.GetString("images").Trim();
                    if (imageStr.Length > 0)
                    {
                        string[] images = imageStr.Split('|');
                        foreach (string image in images)
                        {
                            if (image.Trim().Length > 0)
                            {
                                model.Product.Images.Add(image);
                            }
                        }
                    }
                }
            }, sql, new SqlParameter("@id", id));

            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                while (await reader.ReadAsync())
                {
                    ProductCategory ct = new ProductCategory();
                    ct.Id = reader.GetInt32(0);
                    ct.Name = reader.GetString(1);
                    model.Categories.Add(ct);
                }
            }, "SELECT id, name FROM categories ORDER BY id ASC");

            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                while (await reader.ReadAsync())
                {
                    model.Units.Add(reader.GetString(0));
                }
            }, "SELECT DISTINCT unit FROM products WHERE NOT unit = ''");
            
            return Results.Ok(model);
        }
        private static async Task<IResult> CreateAsync(Product product, HttpContext context, DbClient db)
        {
            string sql = """
                INSERT INTO products
                	([sku], [name], [description], [category], [stock], [minstock], [maxstock], [unit], [costAverage], [price], [isActive], [images], [createdBy])
                VALUES 
                	(@sku, @name, @description, @category, @stock, @minstock, @maxstock, @unit, @costAverage, @price, @isActive, @images, @createdBy);
                SELECT SCOPE_IDENTITY()
                """;
            int userID = AppHelpers.GetUserID(context);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@sku", product.SKU),
                new SqlParameter("@name", product.Name),
                new SqlParameter("@description", product.Description),
                new SqlParameter("@category", product.Category),
                new SqlParameter("@stock", product.Stock),
                new SqlParameter("@minstock", product.MinStock),
                new SqlParameter("@maxstock", product.MaxStock),
                new SqlParameter("@unit", product.Unit),
                new SqlParameter("@costAverage", product.BasicPrice),
                new SqlParameter("@price", product.Price),
                new SqlParameter("@isActive", product.IsActive),
                new SqlParameter("@images", product.WholesaleQuantity),
                new SqlParameter("@createdBy", product.IsActive)
            };
            product.Id = await db.ExecuteScalarIntegerAsync(sql, parameters);
            return Results.Ok(product);
        }
        private static async Task<IResult> UpdateAsync(Product product, HttpContext context, DbClient db)
        {
            string sql = """
                                UPDATE products
                SET [sku] = @sku, [name]=@name, [description]=@description, category=@category,
                [stock]=@stock, minstock=@minstock, maxstock=@maxstock, unit=@unit, costAverage=@costAverage,
                [price]=@price, isActive=@isactive, images=@images, editedBy=@editor, editedDate=GETDATE()
                WHERE [id]=@id
                """;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", product.Id),
                new SqlParameter("@sku", product.SKU),
                new SqlParameter("@name", product.Name),
                new SqlParameter("@description", product.Description),
                new SqlParameter("@category", product.Category),
                new SqlParameter("@stock", product.Stock),
                new SqlParameter("@minstock", product.MinStock),
                new SqlParameter("@maxstock", product.MaxStock),
                new SqlParameter("@unit", product.Unit),
                new SqlParameter("@costAverage", product.BasicPrice),
                new SqlParameter("@price", product.Price),
                new SqlParameter("@isActive", product.IsActive),
                new SqlParameter("@images", string.Join("|", product.Images)),
                new SqlParameter("@editor", AppHelpers.GetUserID(context))
            };
            var success = await db.ExecuteNonQueryAsync(sql, parameters);
            return Results.Ok(new CommonResult() { Success = success });
        }
        private static async Task<IResult> DeleteAsync(int id, HttpContext context, DbClient db)
        {
            int userID = AppHelpers.GetUserID(context);
            string sql = "UPDATE products SET deleted = 1, editedBy=@editor WHERE id=@id";
            var success = await db.ExecuteNonQueryAsync(sql, new SqlParameter("@id", id), new SqlParameter("@editor", userID));
            return Results.Ok(new CommonResult() { Success = success });
        }
    }
}
