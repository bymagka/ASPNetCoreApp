using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ASPNetCoreApp.Services.Interfaces;
using ASPNetCoreApp.Services;
using ASPNetCoreApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreApp.Data;
using ASPNetCoreApp.Services.InSQL;

namespace ASPNetCoreApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ASPNetCoreAPPDb>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TininBase")));
            services.AddTransient<DbInitializer>();

            //services.AddSingleton<IEmployeeService, EmployeesManagementService>();
            //services.AddSingleton<IProductData, ProductDataManagementService>();
            services.AddScoped<IEmployeeService, SQLEmployyesManagementService>();
            services.AddScoped<IProductData, SQLProductDataService>();
          

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
