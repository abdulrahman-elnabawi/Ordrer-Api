using Core.DbContexts;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				if(context.ProductBrands != null && !context.ProductBrands.Any())
				{
					var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null)
                    {
                        foreach (var brand in brands)
                            await context.ProductBrands.AddAsync(brand);

                        await context.SaveChangesAsync();
                    }
					
					
				}
                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Infrastructure/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    if (types is not null)
                    {
                        foreach (var type in types)
                            await context.ProductTypes.AddAsync(type);

                        await context.SaveChangesAsync();
                    }

                    
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Infrastructure/SeedData/Products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (Products is not null)
                    {
                        foreach (var Product in Products)
                            await context.Products.AddAsync(Product);

                        await context.SaveChangesAsync();
                    }
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Infrastructure/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    if (deliveryMethods is not null)
                    {
                        foreach (var deliveryMethod in deliveryMethods)
                            await context.DeliveryMethods.AddAsync(deliveryMethod);

                        await context.SaveChangesAsync();
                    }


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
