using ASPNetCoreApp.DAL.Context;
using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Services.Data;
using ASPNetCoreApp.Services.InCookies;
using ASPNetCoreApp.Services.InSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.WebAPI
{
    public record Startup(IConfiguration configuration)
    {

     
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ASPNetCoreAPPDb>(opt => opt.UseSqlServer(configuration.GetConnectionString("TininBase")));

            services.AddScoped<DbInitializer>();

            

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

            services.AddScoped<IEmployeeService, SQLEmployyesManagementService>();
            services.AddScoped<IProductData, SQLProductDataService>();
            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddScoped<IOrderService, SQLOrderService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASPNetCore.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASPNetCore.WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
