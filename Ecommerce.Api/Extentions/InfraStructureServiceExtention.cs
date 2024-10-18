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
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Shared;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Ecommerce.Api.Extentions
{
    public static class InfraStructureServiceExtention
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.Configure<Jwtoptions>(configuration.GetSection("JwtOptions"));//you register or configure the properties inside the class to be taken from section JwtOptions in app serttings
            var jwtoptions = configuration.GetSection("JwtOptions").Get<Jwtoptions>();
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;//when request fail he will take this new action
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtoptions.Issuer,
                    ValidAudience = jwtoptions.Audiance,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.MySecretKey)),

                    ClockSkew = TimeSpan.FromDays(7)






                };
            });
            Services.AddAuthorization();
          


            //////////////////////////////////////////////////////////////////
            Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

                options.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<StoreIdentityContext>().AddDefaultTokenProviders();//for rest of services;
            Services.AddScoped<IAuthenticationService, AuthenticatinService>();


            //////////////////////////////////////////////////////////////////////
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<I_DbInitializer, DbInitializer>();
            Services.AddScoped<IBacketRepository, BacketRepository>();
            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection"));
                //configurations to deal with appsettings 
            });
            Services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentitySqlConnection"));
                //configurations to deal with appsettings 
            });
            
            Services.AddSingleton<IConnectionMultiplexer>(_=>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            // it takes from me Iservice Provider and return ConnectionMultiplexer but i dosent need that service so i can discard it by _

            return Services;
        }
    }
}
