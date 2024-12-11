using AdminDashboard.DocumentService;
using AdminDashboard.Models;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;
using Persistance.Repositories;
using Services.MappingProfiles;

namespace AdminDashboard
{
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"));
            });
            builder.Services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySqlConnection"));
            });


            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

                options.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<StoreIdentityContext>().AddDefaultTokenProviders();//for rest of services;

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
          

     /*       builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);*/

            builder.Services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);

            builder.Services.AddAutoMapper(p =>
            {

                p.AddProfile<AdminProductProfile>();

            });

            builder.Services.AddScoped<IDocummentService, DocummentService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = new PathString("Home/Error");
                //it goes instead of 403 forbidden 
                options.LoginPath = new PathString("Admin/Login");
                //if you dont you must make the Admin controller named Account to go by defult to Account/Login
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
