
using System.Collections.Generic;
using System.Linq;


namespace ASPNetCoreApp.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel product, int Quantity)> ItemsList { get; set; }

        public int ItemsCount => ItemsList?.Sum(x => x.Quantity) ?? 0;

        public decimal TotalPrice => ItemsList?.Sum(x => x.product.Price * x.Quantity) ?? 0;
    }
}
