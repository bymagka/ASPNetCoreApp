using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.DAL.Context;
using ASPNetCoreApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Services.InSQL
{
    public class SQLProductDataService : IProductData
    {
        private readonly ASPNetCoreAPPDb db;

        public SQLProductDataService(ASPNetCoreAPPDb context)
        {
            db = context;
        }

        public IEnumerable<Brand> GetBrands() => db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = db.Products;

            if (filter?.BrandId != null)
                query = query.Where(x => x.BrandId == filter.BrandId);

            if (filter?.SectionId != null)
                query = query.Where(x => x.SectionId == filter.SectionId);

            return query;
        }

        public IEnumerable<Section> GetSections() => db.Sections;
    }
}
