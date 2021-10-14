
using ASPNetCoreApp.ViewModels;
using ASPNetCoreApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetCoreApp.Infostructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product prod) =>
            prod is null ? null : new ProductViewModel
            {
                Id = prod.Id,
                ImageUrl = prod.ImageUrl,
                Name = prod.Name,
                Price = prod.Price,
                Brand = prod.Brand?.Name,
                Section = prod.Section?.Name,

            };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);

    }

    public static class OrderMapper
    {
        public static OrderViewModel ToView(this Order order)
        {
            return order is null ? null : new OrderViewModel
            {

            };
        }

        public static IEnumerable<OrderViewModel> ToView(this IEnumerable<Order> orders) => orders.Select(ToView);
    }
}
