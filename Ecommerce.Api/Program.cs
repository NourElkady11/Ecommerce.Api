
using Domain.Contracts;
using Ecommerce.Api.Extentions;
using Ecommerce.Api.Factories;
using Ecommerce.Api.Midlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddPresentationServices();
            #endregion

            var app = builder.Build();

            #region Middlewares
            await app.SeedDbAsync();
            app.UseCustomExeptionMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion
            app.Run();

        }

    
    }
}
