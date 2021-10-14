using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Services.Interfaces
{
    interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName,CartViewModel Cart,OrderViewModel OrderModel);
    }
}
