using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DbInitializer : I_DbInitializer
    {
        private readonly StoreContext storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if ((await storeContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await storeContext.Database.MigrateAsync();
                }

                if (!storeContext.productTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                        await storeContext.productTypes.AddRangeAsync(types);
                        await storeContext.SaveChangesAsync();
                    }
                }

                if (!storeContext.productBrands.Any())
                {
                    var BrandsData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    if (brands is not null && brands.Any())
                    {
                        await storeContext.productBrands.AddRangeAsync(brands);
                        await storeContext.SaveChangesAsync();
                    }
                }


                if (!storeContext.products.Any())
                {
                    var ProductsData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (products is not null && products.Any())
                    {
                        await storeContext.products.AddRangeAsync(products);
                        await storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex) 
            { 
                  
            }
        

        }
    }
}
//C:\Users\Actel\OneDrive\Desktop\.Net\Projects\Ecommerce.Api