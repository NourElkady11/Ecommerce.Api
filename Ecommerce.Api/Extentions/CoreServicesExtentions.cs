using Services.Abstractions;
using Services;
using Shared;
using Domain.Contracts;
using Persistance.Repositories;
using Persistance;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;
using StackExchange.Redis;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Ecommerce.Api.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IServiceManger, ServiceManger>();
            services.AddScoped<IAuthenticationService, AuthenticatinService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<I_DbInitializer, DbInitializer>();
            services.AddScoped<IBacketRepository, BacketRepository>();
            services.AddScoped<IChacheRepository, CacheRepository>();
            services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);

            services.Configure<Jwtoptions>(configuration.GetSection("JwtOptions"));//you register or configure the properties inside the class to be taken from section JwtOptions in app serttings
            var jwtoptions = configuration.GetSection("JwtOptions").Get<Jwtoptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//when request fail he will take this new action
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

                    /*ClockSkew = TimeSpan.FromSeconds(20)*/
                };
            });

            /*   Services.AddAuthorization();*/
            //////////////////////////////////////////////////////////////////
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

                options.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<StoreIdentityContext>().AddDefaultTokenProviders();//for rest of services;


            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection"));
                //configurations to deal with appsettings 
            });
            services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentitySqlConnection"));
                //configurations to deal with appsettings 
            });

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));


            return services;

        }
    }
}
