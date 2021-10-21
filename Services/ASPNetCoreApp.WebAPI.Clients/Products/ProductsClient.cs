using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.DTO;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.WebAPI.Clients
{
    public class ProductsClient : BaseClient, IProductData
    {

        public ProductsClient(HttpClient httpClient) : base(httpClient,"api/products")
        {

        }

        public Brand GetBrandById(int id)
        {
            var brand = Get<BrandDTO>($"{Adress}/brands/{id}");

            return brand.FromDTO();
        }

        public IEnumerable<Brand> GetBrands()
        {
            var brands = Get<IEnumerable<BrandDTO>>($"{Adress}/brands");

            return brands.FromDTO();
        }

        public Product GetProductById(int id)
        {
            var product = Get<ProductDTO>($"{Adress}/{id}");

            return product.FromDTO();
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            var response = Post($"{Adress}", filter ?? new ProductFilter());

            var result = response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result;

            return result.FromDTO();
        }

        public Section GetSectionById(int id)
        {
            var section = Get<SectionDTO>($"{Adress}/sections/{id}");

            return section.FromDTO();
        }

        public IEnumerable<Section> GetSections()
        {
            var sections = Get<IEnumerable<SectionDTO>>($"{Adress}/sections");

            return sections.FromDTO();
        }
    }
}
