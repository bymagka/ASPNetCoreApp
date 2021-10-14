using Microsoft.EntityFrameworkCore;
using ASPNetCoreApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASPNetCoreApp.Domain.Identity;
using System;

namespace ASPNetCoreApp.DAL.Context
{
    public class ASPNetCoreAPPDb : IdentityDbContext<User,Role,string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public ASPNetCoreAPPDb(DbContextOptions options) : base(options)
        {

        }

    }
}
