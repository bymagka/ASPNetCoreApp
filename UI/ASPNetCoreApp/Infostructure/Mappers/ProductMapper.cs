
using ASPNetCoreApp.Domain.ViewModels;
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
                Adress = order.Adress,
                Phone = order.Phone,
                Description = order.Description,
            };
        }


        public static IEnumerable<OrderViewModel> ToView(this IEnumerable<Order> orders) => orders.Select(ToView);

        public static UserOrderViewModel ToUserView(this Order order)
        {
            return order is null ? null : new UserOrderViewModel
            {
                Address = order.Adress,
                Phone = order.Phone,
                Date = order.Date,
                Id = order.Id,
                Description = order.Description,
                TotalPrice = order.Items.Sum(x => x.TotalPrice)
        };
    }


        public static IEnumerable<UserOrderViewModel> ToUserView(this IEnumerable<Order> orders) => orders.Select(ToUserView);
    }
}
