
using Domain.Contracts;
using Ecommerce.Api.Factories;
using Ecommerce.Api.Midlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using Services;
using Services.Abstractions;
using System.Reflection;
using System.Reflection.Metadata;

namespace Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services
            builder.Services.AddControllers().AddApplicationPart(typeof(Presentaion.AssemblyRefrence).Assembly);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);
            builder.Services.AddScoped<I_DbInitializer, DbInitializer>();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"));
            });
            builder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactrory.CustomValidationErrorResponse;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion
            var app = builder.Build();
            await SeedDatabaseAsync(app);
            #region Middlewares
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();

            async Task SeedDatabaseAsync(WebApplication app)
            {
                using var scope = app.Services.CreateScope();

                var dbInitializer = scope.ServiceProvider.GetRequiredService<I_DbInitializer>();

                await dbInitializer.InitializeAsync();
            }
        }

    
    }
}
