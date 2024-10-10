using Services.Abstractions;
using Services;

namespace Ecommerce.Api.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManger, ServiceManger>();
            services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);
            return services;
        }
    }
}
