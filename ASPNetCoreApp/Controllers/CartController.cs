using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Services.InCookies;
using ASPNetCoreApp.Services.Interfaces;

namespace ASPNetCoreApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Add(int id)
        {
            cartService.Add(id);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            cartService.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            cartService.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Decrement(int id)
        {
            cartService.Decrement(id);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(cartService.GetViewModel());
        }
    }
}
