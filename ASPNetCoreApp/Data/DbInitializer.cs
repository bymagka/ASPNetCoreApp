using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreApp.Data
{
    public class DbInitializer
    {
        private readonly ASPNetCoreAPPDb dbContext;

        public DbInitializer(ASPNetCoreAPPDb context)
        {
            dbContext = context;
        }

        public async Task InitializeAsync()
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync();

            if(pendingMigrations.Any())
                 await dbContext.Database.MigrateAsync();

            await InitializeProductsAsync();
        }



        public async Task InitializeProductsAsync()
        {
            await using (await dbContext.Database.BeginTransactionAsync())
            {

                dbContext.Sections.AddRange(TestData.Sections);

                await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await dbContext.SaveChangesAsync();
                await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                await dbContext.Database.CommitTransactionAsync();
            }

            await using (await dbContext.Database.BeginTransactionAsync())
            {

                dbContext.Brands.AddRange(TestData.Brands);

                await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await dbContext.SaveChangesAsync();
                await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                await dbContext.Database.CommitTransactionAsync();
            }

            await using (await dbContext.Database.BeginTransactionAsync())
            {

                dbContext.Products.AddRange(TestData.Products);

                await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await dbContext.SaveChangesAsync();
                await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                await dbContext.Database.CommitTransactionAsync();
            }
        }
    }
}
