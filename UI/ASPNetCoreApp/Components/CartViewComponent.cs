using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService cartService;

        public CartViewComponent(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Count = cartService.GetViewModel().ItemsCount;

            return View();
        }
    }
}
