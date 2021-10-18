using ASPNetCoreApp.Domain.Entities;
using System.Collections.Generic;
using ASPNetCoreApp.Domain;

namespace ASPNetCoreApp.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Brand> GetBrands();

        IEnumerable<Section> GetSections();

        IEnumerable<Product> GetProducts(ProductFilter filter = null);

        Product GetProductById(int id);

        Brand GetBrandById(int id);

        Section GetSectionById(int id);
    }
}
