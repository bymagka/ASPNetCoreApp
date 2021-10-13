using ASPNetCoreApp.Infostructure.Mappers;
using ASPNetCoreApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private IProductData productData;

        public ProductsController(IProductData productData)
        {
            this.productData = productData;
        }

        public IActionResult Index()
        {
            
            return View(productData.GetProducts().ToView());
        }
    }
}
