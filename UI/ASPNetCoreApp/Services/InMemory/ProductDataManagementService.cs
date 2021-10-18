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

            if(filter.Ids.Length > 0)
            {
                query.Where(x => filter.Ids.Contains(x.Id));
            }
            else
            {
                if (filter?.BrandId != null)
                    query = query.Where(x => x.BrandId == filter.BrandId);

                if (filter?.SectionId != null)
                    query = query.Where(x => x.SectionId == filter.SectionId);
            }


            return query;
        }

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(prod => prod.Id == id);

        public Brand GetBrandById(int id) => TestData.Brands.FirstOrDefault(br => br.Id == id);

        public Section GetSectionById(int id) => TestData.Sections.FirstOrDefault(br => br.Id == id);
    
    }
}
