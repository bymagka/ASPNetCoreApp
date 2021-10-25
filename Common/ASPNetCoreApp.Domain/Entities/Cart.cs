using System.Collections.Generic;
using System.Linq;


namespace ASPNetCoreApp.Domain.Entities
{    /// <summary>
     /// Корзина
     /// </summary>
    public class Cart
    {

        /// <summary>
        /// Элементы корзины
        /// </summary>
        public ICollection<CartItem> CartItems = new List<CartItem>();

        /// <summary>
        /// Количество элементов корзины
        /// </summary>
        public int ItemsCount => CartItems?.Sum(it => it.Quantity) ?? 0;
    }

    /// <summary>
    /// Элемент корзины
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }
    }
}
