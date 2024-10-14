using Services.Abstractions;
using Services;
using Domain.Contracts;
using Ecommerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Persistance.Data;
using Persistance.Repositories;
using Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;

namespace Ecommerce.Api.Extentions
{
    public static class InfraStructureServiceExtention
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<I_DbInitializer, DbInitializer>();
            Services.AddScoped<IBacketRepository, BacketRepository>();

            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection"));
                //configurations to deal with appsettings 
            });
            
            Services.AddSingleton<IConnectionMultiplexer>(_=>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            // it takes from me Iservice Provider and return ConnectionMultiplexer but i dosent need that service so i can discard it by _

            return Services;
        }
    }
}
