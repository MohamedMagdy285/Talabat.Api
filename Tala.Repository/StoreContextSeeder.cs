using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Tala.Repository
{
    public static class StoreContextSeeder
    {
        public static async Task SeedAsync(StoreContext context)
        {
            #region ProductBrands
            if (!context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("../Tala.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                        await context.Set<ProductBrand>().AddAsync(brand);
                    await context.SaveChangesAsync();
                }
            }
            #endregion

            #region ProductTypes Insertion
            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Tala.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Count > 0)
                {
                    foreach (var type in types)
                        await context.Set<ProductType>().AddAsync(type);

                    await context.SaveChangesAsync();
                }
            }

            #endregion

            #region Product Insertion
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Tala.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                        await context.Set<Product>().AddAsync(product);

                    await context.SaveChangesAsync();
                }
            }

            #endregion
        }
    }
}
