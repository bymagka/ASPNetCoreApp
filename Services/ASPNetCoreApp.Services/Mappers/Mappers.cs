
using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Domain.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetCoreApp.Services.Mappers
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



        public static ProductDTO ToDTO(this Product product)
        {
            return product is null ? null : new ProductDTO
            {
                Id = product.Id,
                Order = product.Order,
                Name = product.Name,
                Price = product.Price,
                Brand = product.Brand.ToDTO(),
                Section = product.Section?.ToDTO(),
                ImageURL = product.ImageUrl,
            };
        }

        public static Product FromDTO(this ProductDTO product)
        {
            return product is null ? null : new Product
            {
                Id = product.Id,
                Order = product.Order,
                Name = product.Name,
                Price = product.Price,
                BrandId = product.Brand.Id,
                Brand = product.Brand.FromDTO(),
                SectionId = product.Section?.Id,
                Section = product.Section.FromDTO(),
                ImageUrl = product.ImageURL,
            };
        }

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) => products.Select(ToDTO);

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> products) => products.Select(FromDTO);



    }

    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand brand)
        {
            return brand is null ? null : new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
            };
        }

        public static Brand FromDTO(this BrandDTO brand)
        {
            return brand is null ? null : new Brand
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
            };
        }


        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> brands) => brands.Select(ToDTO);

        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> brands) => brands.Select(FromDTO);
    }

    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section section)
        {
            return section is null ? null : new SectionDTO
            {
                Id = section.Id,
                Order = section.Order,
                Name = section.Name,
                ParentId = section.ParentId,
            };
        }

        public static Section FromDTO(this SectionDTO section)
        {
            return section is null ? null : new Section
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId,
            };
        }

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> sections) => sections.Select(ToDTO);

        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> sections) => sections.Select(FromDTO);
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


        #region DTO
        public static OrderDTO ToDTO(this Order order)
        {
            return order is null ? null : new OrderDTO
            {
                Id = order.Id,
                Adress = order.Adress,
                Date = order.Date,
                Description = order.Description,
                Phone = order.Phone,
                Items = order.Items.ToDTO(),
                
            };
        }

        public static Order FromDTO(this OrderDTO order)
        {
            return order is null ? null : new Order
            {
                Id = order.Id,
                Adress = order.Adress,
                Date = order.Date,
                Description = order.Description,
                Phone = order.Phone,
                Items = order.Items.FromDTO().ToList(),

            };
        }

        public static IEnumerable<OrderDTO> ToDTO(this IEnumerable<Order> orders) => orders.Select(ToDTO);

        public static IEnumerable<Order> FromDTO(this IEnumerable<OrderDTO> orders) => orders.Select(FromDTO);
        #endregion
    }

    public static class OrderItemMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem orderItem)
        {
            return orderItem is null ? null : new OrderItemDTO
            {
                Id = orderItem.Id,
                Price = orderItem.Price,
                ProductId = orderItem.Product.Id,
                Quantity = orderItem.Quantity,
            };
        }

        public static OrderItem FromDTO(this OrderItemDTO orderItem)
        {
            return orderItem is null ? null : new OrderItem
            {
                Id = orderItem.Id,
                Price = orderItem.Price,
                Product = new() {Id = orderItem.ProductId},
                Quantity = orderItem.Quantity,
            };
        }

        public static IEnumerable<OrderItemDTO> ToDTO(this IEnumerable<OrderItem> orders) => orders.Select(ToDTO);

        public static IEnumerable<OrderItem> FromDTO(this IEnumerable<OrderItemDTO> orders) => orders.Select(FromDTO);

        public static CartViewModel ToCartView(this IEnumerable<OrderItemDTO> orders)
        {
            return new CartViewModel
            {
                ItemsList = orders.Select(x => (new ProductViewModel
                {
                    Id = x.Id,
                },
                x.Quantity))
            };
        }
    }

    public static class CartViewModelMapper
    {
        public static IEnumerable<OrderItemDTO> ToDTO(this CartViewModel cartViewModel)
        {
            return cartViewModel.ItemsList.Select(x => new OrderItemDTO 
            { 
                Price = x.product.Price,
                ProductId = x.product.Id,
                Quantity = x.Quantity,
            });
        }
    }

    public static class ProductsPageDTOMapper
    {
        public static ProductsPageDTO ToDTO(this ProductsPage Page) => new ProductsPageDTO(Page.Products.ToDTO(), Page.TotalCount);

        public static ProductsPage FromDTO(this ProductsPageDTO Page) => new ProductsPage(Page.Products.FromDTO(), Page.TotalCount);
    }

}
