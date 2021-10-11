using System.Collections.Generic;
using System.Linq;


namespace ASPNetCoreApp.Domain.Entities
{
    public class Cart
    {
        public ICollection<CartItem> CartItems => new List<CartItem>();

        public int ItemsCount => CartItems?.Sum(it => it.Quantity) ?? 0;
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
