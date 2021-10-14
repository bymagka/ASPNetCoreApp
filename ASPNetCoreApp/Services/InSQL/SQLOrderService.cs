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
using Microsoft.EntityFrameworkCore;

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

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await userManager.FindByNameAsync(UserName).ConfigureAwait(false);

            if(user is null)
            {
                throw new InvalidOperationException($"Пользователь {UserName} не найден");
            }

            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            Order order = new Order
            {
                User = user,
                Adress = OrderModel.Adress,
                Description = OrderModel.Description,
                Phone = OrderModel.Phone,

            };

            var product_ids = Cart.ItemsList.Select(item => item.product.Id).ToArray();

            var cartProducts = await dbContext.Products.Where(prod => product_ids.Contains(prod.Id)).ToArrayAsync();

            order.Items = Cart.ItemsList.Join(cartProducts,
                cartItem=>cartItem.product.Id,
                cartProd=>cartProd.Id,
                (cartItem,cartProd)=> new OrderItem { 
                Order = order,
                Product = cartProd,
                Price = cartProd.Price,
                Quantity = cartItem.Quantity
                }).ToArray();

            await dbContext.Orders.AddAsync(order);

            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;

        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(false);

            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            var orders = await dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(o => o.Product)
                .Where(o => o.User.UserName == UserName)
                .ToArrayAsync()
                .ConfigureAwait(false);

            return orders;
                
        }
    }
}
