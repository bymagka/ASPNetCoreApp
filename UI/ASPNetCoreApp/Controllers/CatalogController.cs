using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Services.Mappers;
using Microsoft.Extensions.Configuration;

namespace ASPNetCoreApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IConfiguration configuration;

        public IProductData ProductData { get; }

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            ProductData = productData;
            this.configuration = configuration;
        }
     
        public IActionResult Index(int? BrandId, int? SectionId,int Page = 1,int? PageSize = null)
        {
            var page_size = PageSize ?? (int.TryParse(configuration["CatalogPageSize"], out int value) ? value : null);

            ProductFilter filter = new ProductFilter()
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize = page_size
            };

            var (products,total_count) = ProductData.GetProducts(filter);

            var catalogViewModel = new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products.OrderBy(x => x.Order).ToView()
            };

            return View(catalogViewModel);
        }
   
        public IActionResult Details(int id)
        {
            var prod = ProductData.GetProductById(id);

            if (prod is null) return NotFound();

            return View(prod.ToView());
        }
    }
}
