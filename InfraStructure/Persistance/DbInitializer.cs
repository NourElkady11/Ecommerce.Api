using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System.Text.Json;

namespace Persistance
{
    public class DbInitializer : I_DbInitializer
    {
        private readonly StoreContext storeContext;
        private readonly StoreIdentityContext storeIdentityContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, StoreIdentityContext StoreIdentityContext)
        {
            this.storeContext = storeContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.storeIdentityContext = StoreIdentityContext;
        }

        // Seeding application data (products, brands, types)
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
                    var brandsData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        await storeContext.productBrands.AddRangeAsync(brands);
                        await storeContext.SaveChangesAsync();
                    }
                }

                if (!storeContext.products.Any())
                {
                    var productsData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                        await storeContext.products.AddRangeAsync(products);
                        await storeContext.SaveChangesAsync();
                    }
                }
            
                if (!storeContext.productBrands.Any())
                {
                    var brandsData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        await storeContext.productBrands.AddRangeAsync(brands);
                        await storeContext.SaveChangesAsync();
                    }
                }

                if (!storeContext.DeliveryWays.Any())
                {
                    var Deliveryways = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistance\\Data\\Seeding\\DeliveryWays.json");
                    var DileveyObject = JsonSerializer.Deserialize<List<DeliveryWays>>(Deliveryways);

                    if (DileveyObject is not null && DileveyObject.Any())
                    {
                        await storeContext.DeliveryWays.AddRangeAsync(DileveyObject);
                        await storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, rethrow, etc.)
            }
        }

        // Seeding identity-related data (roles, users)
        public async Task InitializeIdentityAsync()
        {

            if ((await storeIdentityContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await storeIdentityContext.Database.MigrateAsync();
            }

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!userManager.Users.Any())
            {
                var superAdmin = new User
                {
                    FirstName = "Nour",
                    LastName = "Elkady",
                    UserName = "Nourelkady",
                    Email = "nourel2ady@gmail.com",
                    PhoneNumber = "01021707010",
                };

                var admin = new User
                {
                    FirstName = "Nour",
                    LastName = "Khaled",
                    UserName = "NourKhaled",
                    Email = "nourkhaled@gmail.com",
                    PhoneNumber = "01021707010",
                };

                await userManager.CreateAsync(superAdmin, "Elkady123");
                await userManager.CreateAsync(admin, "Nour123");

                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
