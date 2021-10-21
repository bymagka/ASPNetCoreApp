
using ASPNetCoreApp.Domain.ViewModels;
using System.Collections.Generic;

namespace ASPNetCoreApp.Domain.DTO
{
    public class CreateOrderDTO
    {
        public OrderViewModel OrderModel { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
