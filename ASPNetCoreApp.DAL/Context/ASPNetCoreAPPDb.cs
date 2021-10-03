using Microsoft.EntityFrameworkCore;
using ASPNetCoreApp.Domain.Entities;

namespace ASPNetCoreApp.DAL.Context
{
    public class ASPNetCoreAPPDb : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public ASPNetCoreAPPDb(DbContextOptions options) : base(options)
        {

        }

    }
}
