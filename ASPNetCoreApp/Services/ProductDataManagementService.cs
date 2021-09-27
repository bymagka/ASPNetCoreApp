using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Services.Interfaces;
using System.Collections.Generic;
using ASPNetCoreApp.Data;
using ASPNetCoreApp.Domain;
using System.Linq;

namespace ASPNetCoreApp.Services
{
    public class ProductDataManagementService : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null) 
        {
            IEnumerable<Product> query = TestData.Products;

            if (filter?.BrandId != null)
                query = query.Where(x => x.BrandId == filter.BrandId);

            if (filter?.SectionId != null)
                query = query.Where(x => x.SectionId == filter.SectionId);

            return query;
        }
    }
}
