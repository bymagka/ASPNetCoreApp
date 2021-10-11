using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.DAL.Context;
using ASPNetCoreApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreApp.Services.InSQL
{
    public class SQLProductDataService : IProductData
    {
        private readonly ASPNetCoreAPPDb db;

        public SQLProductDataService(ASPNetCoreAPPDb context)
        {
            db = context;
        }

        public Brand GetBrandById(int id) => db.Brands.FirstOrDefault(br => br.Id == id);

        public IEnumerable<Brand> GetBrands() => db.Brands;

        public Product GetProductById(int id) => db.Products
                                                        .Include(prod=>prod.Section)
                                                        .Include(prod=>prod.Brand)
                                                        .FirstOrDefault(prod => prod.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = db.Products.Include(x=>x.Brand).Include(x=>x.Section);

            if (filter?.BrandId != null)
                query = query.Where(x => x.BrandId == filter.BrandId);

            if (filter?.SectionId != null)
                query = query.Where(x => x.SectionId == filter.SectionId);

            return query;
        }

        public Section GetSectionById(int id) => db.Sections.FirstOrDefault(sect => sect.Id == id);

        public IEnumerable<Section> GetSections() => db.Sections;
    }
}
