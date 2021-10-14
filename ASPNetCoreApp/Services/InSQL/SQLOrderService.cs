using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using ASPNetCoreApp.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.ViewModels;

namespace ASPNetCoreApp.Services.InSQL
{
    public class SQLOrderService : IOrderService
    {
        private readonly UserManager<User> userManager;
        private readonly ASPNetCoreAPPDb dbContext;

        public SQLOrderService(UserManager<User> userManager,ASPNetCoreAPPDb dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
