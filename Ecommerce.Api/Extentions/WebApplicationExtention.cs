using Domain.Contracts;
using Ecommerce.Api.Midlewares;

namespace Ecommerce.Api.Extentions
{
    public static class WebApplicationExtention
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var dbInitializer = scope.ServiceProvider.GetRequiredService<I_DbInitializer>();

            await dbInitializer.InitializeAsync();

            return webApplication;
        }



        public static WebApplication UseCustomExeptionMiddleware(this WebApplication webApplication) {

            webApplication.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return webApplication;
        }
    }


   
}
