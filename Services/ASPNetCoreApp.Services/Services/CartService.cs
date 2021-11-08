using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStore cartStore;
        private readonly IProductData productData;



        public CartService(ICartStore cartStore, IProductData productData)
        {
            this.cartStore = cartStore;
            this.productData = productData;

        }

        public void Add(int id)
        {
            Cart _cart = cartStore.Cart;

            var cartItem = _cart.CartItems.FirstOrDefault(ct => ct.ProductId == id);

            if (ReferenceEquals(cartItem, null))
                _cart.CartItems.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                cartItem.Quantity++;

            cartStore.Cart = _cart;
        }

        public void Clear()
        {
            Cart _cart = cartStore.Cart;

            _cart.CartItems.Clear();

            cartStore.Cart = _cart;
        }

        public void Decrement(int id)
        {
            Cart _cart = cartStore.Cart;

            var cartItem = _cart.CartItems.FirstOrDefault(ct => ct.ProductId == id);

            if (ReferenceEquals(cartItem, null)) return;

            if (--cartItem.Quantity <= 0) _cart.CartItems.Remove(cartItem);
            


            cartStore.Cart = _cart;
        }

        public CartViewModel GetViewModel()
        {

            var products = productData.GetProducts(new ProductFilter
            {
                Ids = cartStore.Cart.CartItems.Select(x => x.ProductId).ToArray()
            });

            var views = products.Products.ToView().ToDictionary(x => x.Id);

            return new CartViewModel
            {
                ItemsList = cartStore.Cart.CartItems.Where(item => views.ContainsKey(item.ProductId))
                                                                 .Select(item => (views[item.ProductId], item.Quantity))
            };
        }

        public void Remove(int id)
        {
            Cart _cart = cartStore.Cart;

            var cartItem = _cart.CartItems.FirstOrDefault(ct => ct.ProductId == id);

            if (ReferenceEquals(cartItem, null)) return;

            _cart.CartItems.Remove(cartItem);

            cartStore.Cart = _cart;
        }

    }
}
