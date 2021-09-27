using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Services.Interfaces;
using ASPNetCoreApp.ViewModels;

namespace ASPNetCoreApp.Controllers
{
    public class CatalogController : Controller
    {
        public IProductData ProductData { get; }

        public CatalogController(IProductData productData) => ProductData = productData;
     
        public IActionResult Index(int? BrandId, int? SectionId)
        {

            ProductFilter filter = new ProductFilter()
            {
                BrandId = BrandId,
                SectionId = SectionId
            };

            var products = ProductData.GetProducts(filter);

            var catalogViewModel = new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products.OrderBy(x => x.Order).Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Price = p.Price
                })
            };

            return View(catalogViewModel);
        }
    }
}
