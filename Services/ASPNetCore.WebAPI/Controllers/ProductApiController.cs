using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Services.Mappers;
using ASPNetCoreApp.Services.Infostructure;
using Microsoft.Extensions.Logging;

namespace ASPNetCore.WebAPI.Controllers
{

    [ApiController]
    [Route(WebApiAdresses.Products)]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductData productData;
        private readonly ILogger<ProductApiController> logger;

        public ProductApiController(IProductData productData,ILogger<ProductApiController> logger)
        {
            this.productData = productData;
            this.logger = logger;
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = productData.GetBrands();

            logger.LogInformation("Getting all brands");

            return Ok(brands.ToDTO());
        }
        
        [HttpGet("brands/{id}")]
        public IActionResult GetBrand(int id)
        {
            var brand = productData.GetBrandById(id);

            if (brand is not { })
                logger.LogInformation("Brand {0} was found by id {1}", brand, id);
            else
                logger.LogError("Brand was not found by id {0}", id);

            return brand is null ? NotFound() : Ok(brand.ToDTO());
        }

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = productData.GetSections();

            logger.LogInformation("Getting all sections");

            return Ok(sections.ToDTO());
        }

        [HttpGet("sections/{id}")]
        public IActionResult GetSection(int id)
        {
            var section = productData.GetSectionById(id);

            if (section is not { })
                logger.LogInformation("Section {0} was found by id {1}", section, id);
            else
                logger.LogError("Section was not found by id {0}", id);

            return section is null ? NotFound() :  Ok(section.ToDTO());
        }

        [HttpPost]
        public IActionResult GetProducts(ProductFilter filter)
        {
            var products = productData.GetProducts(filter);

            logger.LogInformation("Getting all products");

            return Ok(products.ToDTO());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = productData.GetProductById(id);

            if (product is not { })
                logger.LogInformation("Product {0} was found by id {1}", product, id);
            else
                logger.LogError("Product was not found by id {0}", id);

            return product is null ? NotFound() : Ok(product.ToDTO());
        }
        
    }
}
