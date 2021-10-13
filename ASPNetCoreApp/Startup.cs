using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ASPNetCoreApp.Services.Interfaces;
using ASPNetCoreApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreApp.Data;
using ASPNetCoreApp.Services.InSQL;
using ASPNetCoreApp.Services.InCookies;
using ASPNetCoreApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;

namespace ASPNetCoreApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ASPNetCoreAPPDb>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TininBase")));
            services.AddTransient<DbInitializer>();


            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ASPNetCoreAPPDb>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(op =>
            {
#if DEBUG
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireUppercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequiredLength = 3;
                op.Password.RequiredUniqueChars = 3;
#endif
                op.User.RequireUniqueEmail = false;
                

                op.Lockout.AllowedForNewUsers = false;
                op.Lockout.MaxFailedAccessAttempts = 10;
                op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(ck =>
            {
                ck.Cookie.Name = "ASPNetCoreApp";
                ck.Cookie.HttpOnly = true;
                ck.ExpireTimeSpan = TimeSpan.FromDays(10);
                ck.LoginPath = "/Account/Login";
                ck.LogoutPath = "/Account/Logout";

                ck.SlidingExpiration = true;
            });

            //services.AddSingleton<IEmployeeService, EmployeesManagementService>();
            //services.AddSingleton<IProductData, ProductDataManagementService>();
            services.AddScoped<IEmployeeService, SQLEmployyesManagementService>();
            services.AddScoped<IProductData, SQLProductDataService>();
            services.AddScoped<ICartService, InCookiesCartService>();
          

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
        }



        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithRedirects("/home/PageNotFound");



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
