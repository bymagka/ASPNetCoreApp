
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
    }
}
