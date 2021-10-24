using ASPNetCoreApp.Interfaces.Services;
using System;
using System.Collections.Generic;
using ASPNetCoreApp.Domain.DTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Services.Mappers;
using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Services.Infostructure;

namespace ASPNetCoreApp.WebAPI.Clients
{
    public class OrdersClient : BaseClient,IOrderService
    {
        public OrdersClient(HttpClient client) : base(client,WebApiAdresses.Orders)
        {

        }

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {

            CreateOrderDTO createOrderModel = new CreateOrderDTO
            {
                Items = Cart.ToDTO(),
                OrderModel = OrderModel,
            };

            var response = await PostAsync($"{Adress}/{UserName}", createOrderModel).ConfigureAwait(false);

            var new_order = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<OrderDTO>().ConfigureAwait(false);

            return new_order.FromDTO();


        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await GetAsync<OrderDTO>($"{id}").ConfigureAwait(false);
            return order.FromDTO();
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            var orders = await GetAsync<IEnumerable<OrderDTO>>($"{Adress}/user/{UserName}").ConfigureAwait(false);
            return orders.FromDTO();
        }
    }
}
