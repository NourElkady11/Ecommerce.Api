using Ecommerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;

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
            Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter Bearer Token",
                    Name = "Authorizartion",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat="JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme()
                    {
                        Reference=new OpenApiReference()
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new List<string>(){}
                    }

                });
            });

            return Services;
        }
    }
}
