using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if(context.ProductBrands != null && context.ProductBrands.Any())
                {
                    //Presist Data to DB
                    var brandsData = File.ReadAllText("../Store.Repoistory/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands is not null)
                        await context.ProductBrands.AddRangeAsync(brands);
                }
                if (context.ProductTypes != null && context.ProductTypes.Any())
                {
                    //Presist Data to DB
                    var typesData = File.ReadAllText("../Store.Repoistory/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null)
                        await context.ProductTypes.AddRangeAsync(types);
                }
                if (context.Products != null && context.Products.Any())
                {
                    //Presist Data to DB
                    var productsData = File.ReadAllText("../Store.Repoistory/SeedData/product.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null)
                        await context.Products.AddRangeAsync(products);
                }
            }
            catch (Exception ex)
            {
                    var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.Message);
            }
        }
    }
}
