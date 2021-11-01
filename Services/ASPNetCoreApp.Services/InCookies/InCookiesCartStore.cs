using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ASPNetCoreApp.Services.InCookies
{
    public class InCookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public string CartName { get; set; }

        public Cart Cart
        {
            get
            {
                var context = httpContextAccessor.HttpContext;

                var cookies = context!.Response.Cookies;

                var cart_cookies = context!.Request.Cookies[CartName];

                if (cart_cookies is null)
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


        public InCookiesCartStore(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext?.User;

            var userName = user.Identity!.IsAuthenticated ? $"{user.Identity.Name}" : null;

            CartName = $"ASPNetCoreApp.Cart{userName}";
        }
    }
}
