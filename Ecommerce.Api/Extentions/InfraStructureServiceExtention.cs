using Services.Abstractions;
using Services;
using Domain.Contracts;
using Ecommerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Persistance.Data;
using Persistance.Repositories;
using Persistance;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Extentions
{
    public static class InfraStructureServiceExtention
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<I_DbInitializer, DbInitializer>();


            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection"));
                //configurations to deal with appsettings 
            });
         

            return Services;
        }
    }
}
