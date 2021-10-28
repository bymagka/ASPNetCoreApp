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
using Microsoft.Extensions.Logging;

namespace ASPNetCore.WebAPI.Controllers
{
    
    [ApiController]
    [Route(WebApiAdresses.Orders)]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrderApiController> logger;

        public OrderApiController(IOrderService orderService,ILogger<OrderApiController> logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }

        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await orderService.GetUserOrders(UserName);

            logger.LogInformation("Getting orders made by {0}",UserName);

            return Ok(orders.ToDTO());
        } 


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await orderService.GetOrderById(id);

            if (order is not { })
                logger.LogInformation("Order {0} was found by id {1}", order, id);
            else
                logger.LogError("Order was not found by id {0}", id);

            return order is null ? NotFound() : Ok(order.ToDTO());
        }

        [HttpPost("{UserName}")]
        public async Task<IActionResult> CreateOrder(string UserName,[FromBody] CreateOrderDTO orderDTO)
        {
            var order = await orderService.CreateOrder(UserName, orderDTO.Items.ToCartView(), orderDTO.OrderModel);

            logger.LogInformation("Getting orders made by {0}", UserName);

            return Ok(order.ToDTO());
        }

    }
}
