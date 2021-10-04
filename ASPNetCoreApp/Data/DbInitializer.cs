using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ASPNetCoreApp.Data
{
    public class DbInitializer
    {

        private readonly ASPNetCoreAPPDb dbContext;
        private readonly ILogger<DbInitializer> logger;

        public DbInitializer(ASPNetCoreAPPDb context,ILogger<DbInitializer> logger)
        {
            dbContext = context;
            this.logger = logger;
        }

        public async Task InitializeAsync()
        {

            logger.LogInformation("Инициализация начата");

            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            //var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync();

            if (pendingMigrations.Any())
            {
                logger.LogInformation("Применение миграций: {0}", string.Join(",", pendingMigrations));

                await dbContext.Database.MigrateAsync();
            }
                 

            await InitializeProductsAsync();
        }



        public async Task InitializeProductsAsync()
        {
            var timer = Stopwatch.StartNew();


            var brand_pool = TestData.Brands.ToDictionary(br => br.Id);
            var section_pool = TestData.Sections.ToDictionary(sect => sect.Id);

            foreach(var child_section in TestData.Sections.Where(x=> x.ParentId is not null))
            {
                child_section.Parent = section_pool[(int)child_section.ParentId!];
            }

            foreach(var sect in TestData.Sections)
            {
                sect.Id = 0;
            }

            foreach(var product in TestData.Products)
            {

                product.Brand = brand_pool[product.BrandId];
                product.Section = section_pool[(int)product.SectionId!];

                product.Id = 0;
                product.BrandId = 0;
                product.SectionId = null;
            }

            foreach(var emp in TestData.EmployeesList)
            {
                emp.Id = 0;
            }

            foreach (var brand_item in TestData.Brands)
            {
                brand_item.Id = 0;
            }

            await using (await dbContext.Database.BeginTransactionAsync())
            {
                if (dbContext.Sections.Any()) logger.LogInformation("Секции уже заполнены");
                else
                {
                    logger.LogInformation("Инициализация секций");

                    dbContext.Sections.AddRange(TestData.Sections);
                }

                if (dbContext.Brands.Any()) logger.LogInformation("Бренды уже заполнены");
                else
                {
                    logger.LogInformation("Инициализация брендов");

                    dbContext.Brands.AddRange(TestData.Brands);
                }


                if (dbContext.Products.Any()) logger.LogInformation("Бренды уже заполнены");
                else
                {
                    logger.LogInformation("Инициализация товаров");

                    dbContext.Products.AddRange(TestData.Products);
                }


                if (dbContext.Employees.Any()) logger.LogInformation("Пользователи уже заполнены");
                else
                {
                    logger.LogInformation("Инициализация пользователей");

                    dbContext.Employees.AddRange(TestData.EmployeesList);
                }

                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }

            logger.LogInformation($"Инициализация прошла за {timer.ElapsedMilliseconds} мс");
        }
    }
}
