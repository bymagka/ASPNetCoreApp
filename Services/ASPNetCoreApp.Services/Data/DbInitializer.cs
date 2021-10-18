using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ASPNetCoreApp.Domain.Identity;

namespace ASPNetCoreApp.Services.Data
{
    public class DbInitializer
    {

        private readonly ASPNetCoreAPPDb dbContext;
        private readonly ILogger<DbInitializer> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public DbInitializer(ASPNetCoreAPPDb context,ILogger<DbInitializer> logger,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            dbContext = context;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

            try
            {
                await InitializeProductsAsync();
            }
            catch (Exception)
            {

                logger.LogInformation("Ошибка инициализации каталога товаров");
                throw;
            }

            try
            {
                await InitializeIdentityAsync();
            }
            catch (Exception)
            {
                logger.LogInformation("Ошибка инициализации каталога пользователей");
            }
           
        }

        private async Task InitializeIdentityAsync()
        {
            var timer = Stopwatch.StartNew();
            logger.LogInformation("Инициализация identity");

            async Task CheckRole(string roleName)
            {
                if(await roleManager.RoleExistsAsync(roleName))
                {
                    logger.LogInformation($"Роль {roleName} существует");
                  
                }
                else
                {
                    logger.LogInformation($"Роль {roleName} не существует");
                    await roleManager.CreateAsync(new Role { Name = roleName });
                    logger.LogInformation($"Роль {roleName} создана");
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if(await userManager.FindByNameAsync(User.Administrator) is null)
            {
                logger.LogInformation($"Пользователь {User.Administrator} не существует");

                User admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await userManager.CreateAsync(admin,User.DefaultAdminPass);

                if (creation_result.Succeeded)
                {
                    logger.LogInformation($"Пользователь {admin.UserName} создан");
                    await userManager.AddToRoleAsync(admin, Role.Administrators);
                    logger.LogInformation($"Пользователю {admin.UserName} добавлена роль {Role.Administrators}");
                }
                else
                {
                    var errors = creation_result.Errors.Select(x => x.Description).ToArray();

                    string errorMessage = $"Ошибка при создании пользователя администратор! Ошибки {string.Join(", ", errors)}";

                    logger.LogInformation(errorMessage);

                    throw new InvalidOperationException(errorMessage);
                }
            }

            logger.LogInformation($"Инициализация identity была произведена за {timer.Elapsed.TotalMilliseconds} мс");
        }

        private async Task InitializeProductsAsync()
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
