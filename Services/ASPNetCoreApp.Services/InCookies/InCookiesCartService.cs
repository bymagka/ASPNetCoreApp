using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using ASPNetCoreApp.Domain.Entities;
using Newtonsoft.Json;
using System.Linq;
using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Services.Mappers;

namespace ASPNetCoreApp.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProductData productData;

        public string CartName  { get; set; }

        public Cart Cart 
        {
            get 
            {
                var context = httpContextAccessor.HttpContext;

                var cookies = context!.Response.Cookies;

                var cart_cookies = context!.Request.Cookies[CartName];

                if(cart_cookies is null)
                {
                    Cart cart = new Cart();

                    cookies.Append(CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCart(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }
            set => ReplaceCart(httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCart(IResponseCookies cookies, string cookies_cart)
        {
            cookies.Delete(CartName);
            cookies.Append(CartName, cookies_cart);
        }

        public InCookiesCartService(IHttpContextAccessor httpContextAccessor, IProductData productData)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.productData = productData;

            var user = httpContextAccessor.HttpContext?.User;

            var userName = user.Identity!.IsAuthenticated ? $"{user.Identity.Name}" : null;

            CartName = $"ASPNetCoreApp.Cart{userName}";
        }

        public void Add(int id)
        {
            Cart _cart = Cart;

            var cartItem = _cart.CartItems.FirstOrDefault(ct => ct.ProductId == id);

            if (ReferenceEquals(cartItem, null))
                _cart.CartItems.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                cartItem.Quantity++;

            Cart = _cart;
        }

        public void Clear()
        {
            Cart _cart = Cart;

            _cart.CartItems.Clear();

            Cart = _cart;
        }

        public void Decrement(int id)
        {
            Cart _cart = Cart;

            var cartItem = _cart.CartItems.FirstOrDefault(ct => ct.ProductId == id);

            if (ReferenceEquals(cartItem, null)) return;

            if (cartItem.Quantity <= 0) _cart.CartItems.Remove(cartItem);
            else cartItem.Quantity--;

            
            Cart = _cart;
        }

        public CartViewModel GetViewModel()
        {

            var products = productData.GetProducts(new ProductFilter {
                Ids = Cart.CartItems.Select(x => x.ProductId).ToArray()
            });

            var views = products.Products.ToView().ToDictionary(x => x.Id);

            return new CartViewModel
            {
                ItemsList = Cart.CartItems.Where(item => views.ContainsKey(item.ProductId))
                                                                 .Select(item => (views[item.ProductId], item.Quantity))
            };
        }

        public void Remove(int id)
        {
            Cart _cart = Cart;

            var cartItem = _cart.CartItems.FirstOrDefault(ct => ct.ProductId == id);

            if (ReferenceEquals(cartItem, null)) return;

            _cart.CartItems.Remove(cartItem);
          
            Cart = _cart;
        }
    }
}
