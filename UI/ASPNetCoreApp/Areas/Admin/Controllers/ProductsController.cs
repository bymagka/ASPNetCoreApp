using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.Infostructure.Mappers;
using ASPNetCoreApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=Role.Administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData productData;

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
