using Ecommerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Extentions
{
    public static class presentationServiceExtention
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactrory.CustomValidationErrorResponse;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddControllers().AddApplicationPart(typeof(Presentaion.AssemblyRefrence).Assembly);
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }
    }
}
