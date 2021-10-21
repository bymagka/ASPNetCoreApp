using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Domain.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}
