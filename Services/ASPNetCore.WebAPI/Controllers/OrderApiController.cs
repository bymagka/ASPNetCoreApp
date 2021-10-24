using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Services.Mappers;
using ASPNetCoreApp.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Services.Infostructure;

namespace ASPNetCore.WebAPI.Controllers
{
    
    [ApiController]
    [Route(WebApiAdresses.Orders)]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderApiController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await orderService.GetUserOrders(UserName);
            return Ok(orders.ToDTO());
        } 


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await orderService.GetOrderById(id);

            return order is null ? NotFound() : Ok(order.ToDTO());
        }

        [HttpPost("{UserName}")]
        public async Task<IActionResult> CreateOrder(string UserName,[FromBody] CreateOrderDTO orderDTO)
        {
            var order = await orderService.CreateOrder(UserName, orderDTO.Items.ToCartView(), orderDTO.OrderModel);

            return Ok(order.ToDTO());
        }

    }
}
