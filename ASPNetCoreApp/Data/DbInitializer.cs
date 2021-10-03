using System;
using System.Collections.Generic;
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

            if (dbContext.Sections.Any())
            {
                logger.LogInformation("Секции уже заполнены");
                
            }
            else
            {
                logger.LogInformation("Инициализация секций");
                await using (await dbContext.Database.BeginTransactionAsync())
                {

                    dbContext.Sections.AddRange(TestData.Sections);

                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                    await dbContext.Database.CommitTransactionAsync();
                }
                logger.LogInformation("Инициализация секций выполнена успешно");
            }



            if (dbContext.Brands.Any())
            {
                logger.LogInformation("Бренды уже заполнены");

            }
            else
            {
                logger.LogInformation("Инициализация брендов");
                await using (await dbContext.Database.BeginTransactionAsync())
                {

                    dbContext.Brands.AddRange(TestData.Brands);

                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                    await dbContext.Database.CommitTransactionAsync();
                }
                logger.LogInformation("Инициализация брендов выполнена успешно");
            }


            if (dbContext.Products.Any())
            {
                logger.LogInformation("Бренды уже заполнены");

            }
            else
            {
                logger.LogInformation("Инициализация товаров");
                await using (await dbContext.Database.BeginTransactionAsync())
                {

                    dbContext.Products.AddRange(TestData.Products);

                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
                    await dbContext.Database.CommitTransactionAsync();
                }
                logger.LogInformation("Инициализация товаров выполнена успешно");
            }
        }
    }
}
