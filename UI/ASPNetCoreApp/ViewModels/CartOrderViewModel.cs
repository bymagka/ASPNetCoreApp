using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.ViewModels
{
    public class CartOrderViewModel
    {
        public CartViewModel CartViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; } = new OrderViewModel();
    }
}
