using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Services.InCookies;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
            return View(new CartOrderViewModel { CartViewModel = cartService.GetViewModel() }); ;
        }


        [Authorize]
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderViewModel OrderModel, [FromServices] IOrderService orderService)
        {
            var order = await orderService.CreateOrder(User.Identity!.Name, cartService.GetViewModel(), OrderModel);

            cartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }


        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region WebAPi

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddAPI(int id)
        {
            cartService.Add(id);

            return Json(new { id, message = $"Товар с id {id} добавлен в корзину" });
        }

        public IActionResult DecrementAPI(int id)
        {
            cartService.Decrement(id);

            return Json(new { id, message = $"Количество товара с id {} было уменьшено на 1" });
        }

        public IActionResult RemoveAPI(int id)
        {
            cartService.Remove(id);

            return Ok();
        }

        #endregion

    }
}
