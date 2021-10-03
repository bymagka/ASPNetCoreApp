using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ASPNetCoreApp.Services.Interfaces;
using ASPNetCoreApp.Services;
using ASPNetCoreApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ASPNetCoreAPPDb>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TininBase")));

            services.AddSingleton<IEmployeeService, EmployeesManagementService>();
            services.AddSingleton<IProductData, ProductDataManagementService>();
          

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
            app.UseStatusCodePagesWithRedirects("/home/PageNotFound");
            

            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
