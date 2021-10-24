using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Services.Mappers;
using ASPNetCoreApp.Services.Infostructure;


namespace ASPNetCore.WebAPI.Controllers
{

    [ApiController]
    [Route(WebApiAdresses.Products)]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductData productData;

        public ProductApiController(IProductData productData)
        {
            this.productData = productData;
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = productData.GetBrands();

            return Ok(brands.ToDTO());
        }
        
        [HttpGet("brands/{id}")]
        public IActionResult GetBrand(int id)
        {
            var brand = productData.GetBrandById(id);

            return brand is null ? NotFound() : Ok(brand.ToDTO());
        }

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = productData.GetSections();

            return Ok(sections.ToDTO());
        }

        [HttpGet("sections/{id}")]
        public IActionResult GetSection(int id)
        {
            var section = productData.GetSectionById(id);

            return section is null ? NotFound() :  Ok(section.ToDTO());
        }

        [HttpPost]
        public IActionResult GetProducts(ProductFilter filter)
        {
            var products = productData.GetProducts(filter);

            return Ok(products.ToDTO());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = productData.GetProductById(id);

            return product is null ? NotFound() : Ok(product.ToDTO());
        }
        
    }
}
