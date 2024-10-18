using Services.Abstractions;
using Services;
using Shared;

namespace Ecommerce.Api.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IServiceManger, ServiceManger>();
            services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);
          
            return services;

        }
    }
}
