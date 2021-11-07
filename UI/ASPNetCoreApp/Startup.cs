using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Services.InCookies;
using ASPNetCoreApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using ASPNetCoreApp.Services.Infostructure;
using Microsoft.Extensions.Logging;
using ASPNetCoreApp.Logger;
using ASPNetCoreApp.Services.Services;


namespace ASPNetCoreApp
{
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddIdentity<User, Role>()
                    .AddIdentityAppWebApiClients()
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


            services.AddScoped<ICartStore, InCookiesCartStore>();
            services.AddScoped<ICartService, CartService>();
        

            

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
        }


      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            factory.AddLog4Net();

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
