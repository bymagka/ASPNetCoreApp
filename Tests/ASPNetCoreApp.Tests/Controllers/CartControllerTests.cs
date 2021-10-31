using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Xunit.Assert;
using Moq;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Controllers;
using ASPNetCoreApp.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ASPNetCoreApp.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public async Task Checkout_Call_Service_And_Returns_Redirect()
        {
           

            const string expected_user = "Test user";
            const string expected_description = "Test description";
            const string expected_address = "Test address";
            const string expected_phone = "Test phone";

            Mock<ICartService> cart_service_mock = new Mock<ICartService>();

            cart_service_mock
                .Setup(s => s.GetViewModel())
                .Returns(new CartViewModel
                {
                    ItemsList = new[] { (new ProductViewModel { Name = "Testproduct" },1) },
                });


            const int expected_order_id = 1;
            var order_service_mock = new Mock<IOrderService>();

            order_service_mock
                .Setup(s => s.CreateOrder(It.IsAny<string>(), It.IsAny<CartViewModel>(), It.IsAny<OrderViewModel>()))
                .ReturnsAsync(new Order 
                { 
                    Id = expected_order_id,
                    Description = expected_description,
                    Adress = expected_address,
                    Phone = expected_phone,
                    Date = DateTime.Now,
                    Items = Array.Empty<OrderItem>(),
                });

            var cart_controller = new CartController(cart_service_mock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new [] { new Claim(ClaimTypes.Name, expected_user) }))
                    }
                }
            };

            var order_model = new OrderViewModel
            {
                Adress =expected_address,
                Phone = expected_phone,
                Description = expected_description,
            };

            var result = await cart_controller.Checkout(order_model, order_service_mock.Object);

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(CartController.OrderConfirmed), redirect_result.ActionName);

            Assert.Null(redirect_result.ControllerName);

            Assert.Equal(expected_order_id,redirect_result.RouteValues["id"]);

            order_service_mock
                .Verify(s => s.CreateOrder(It.IsAny<string>(), It.IsAny<CartViewModel>(), It.IsAny<OrderViewModel>()));
            cart_service_mock.Verify(s => s.GetViewModel());
            cart_service_mock.Verify(s => s.Clear());
            order_service_mock.VerifyNoOtherCalls();
            cart_service_mock.VerifyNoOtherCalls();

            order_service_mock.VerifyAll();
            cart_service_mock.VerifyAll();

        }

        

        
    }
}
