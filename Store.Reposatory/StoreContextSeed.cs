using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Reposatory
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext Context , ILoggerFactory loggerfactory)
        {
            try
            {
                if(Context.productBrands!=null&& !Context.productBrands.Any()) {

                    var brandsData = File.ReadAllText("../Store.Reposatory/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if(brands is not null)
                    {
                       
                         await Context.productBrands.AddRangeAsync(brands);
                  
                        await Context.SaveChangesAsync();
                    }
                }
                if (Context.ProductTypes != null && !Context.ProductTypes.Any())
                {

                    var typsData = File.ReadAllText("../Store.Reposatory/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(typsData);
                    if (Types is not null)
                    {
                     
                         await Context.ProductTypes.AddRangeAsync(Types);

                        await Context.SaveChangesAsync();
                    }
                }
                if (Context.Products != null && !Context.Products.Any())
                {

                    var productsData = File.ReadAllText("../Store.Reposatory/SeedData/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (product is not null)
                    {

                        await Context.Products.AddRangeAsync(product);

                        await Context.SaveChangesAsync();
                    }
                }
                if (Context.delivaryMethods != null && !Context.delivaryMethods.Any())
                {

                    var delivaryMethodsData = File.ReadAllText("../Store.Reposatory/SeedData/delivery.json");
                    var delivaryMethods = JsonSerializer.Deserialize<List<DelivaryMethod>>(delivaryMethodsData);
                    if (delivaryMethods is not null)
                    {

                        await Context.delivaryMethods.AddRangeAsync(delivaryMethods);

                        await Context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex) {
                var logger=loggerfactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}
