using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Domain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }

        public string Phone { get; set; }

        public DateTimeOffset Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }

    }
}
