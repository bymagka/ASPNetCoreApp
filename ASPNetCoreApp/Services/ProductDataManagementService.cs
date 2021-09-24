using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Services.Interfaces;
using System.Collections.Generic;
using ASPNetCoreApp.Data;

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
    }
}
