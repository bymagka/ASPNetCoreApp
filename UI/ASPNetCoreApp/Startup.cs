using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreApp.Services.Data;
using ASPNetCoreApp.Services.InSQL;
using ASPNetCoreApp.Services.InCookies;
using ASPNetCoreApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using ASPNetCoreApp.Interfaces.TestApi;
using ASPNetCoreApp.WebAPI.Clients;
using ASPNetCoreApp.WebAPI.Clients.Identity;

namespace ASPNetCoreApp
{
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddIdentity<User, Role>()
                    //.AddEntityFrameworkStores<ASPNetCoreAPPDb>()
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


            services.AddScoped<ICartService, InCookiesCartService>();
        

            services.AddHttpClient("ASPNetCoreWebAPI", client => client.BaseAddress = new(Configuration["WebAPI"]))
                .AddTypedClient<IValuesService, ValuesClient>()
                .AddTypedClient<IEmployeeService, EmployyesClient>()
                .AddTypedClient<IProductData, ProductsClient>()
                .AddTypedClient<IOrderService, OrdersClient>()
                .AddTypedClient<IUserStore<User>, UsersClient>()
                .AddTypedClient<IUserRoleStore<User>, UsersClient>()
                .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
                .AddTypedClient<IUserClaimStore<User>, UsersClient>()
                .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTypedClient<IUserLoginStore<User>, UsersClient>()
                .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTypedClient<IRoleStore<Role>, RolesClient>();

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
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
