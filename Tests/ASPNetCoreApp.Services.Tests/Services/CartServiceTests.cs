using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Assert = Xunit.Assert;


namespace ASPNetCoreApp.Services.Tests.Services
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _Cart;

        [TestInitialize]
        public void TestInitialize()
        {
            _Cart = new Cart()
            {
                CartItems = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity= 2},
                    new CartItem(){ProductId= 2, Quantity = 3}

                }
            };
        }

        [TestMethod]
        public void Cart_Class_Returns_Correct_Items_Count() 
        {
            var cart = _Cart;

            var expected_count = cart.CartItems.Sum(x => x.Quantity);

            var actual_count = cart.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Class_Correct_ItemsCount()
        {
            var cart = new CartViewModel
            {
                ItemsList = new List<(ProductViewModel, int)>() 
                {
                    (new ProductViewModel{Id = 1 , Name = "Test product 1"},2),
                    (new ProductViewModel{Id = 2, Name = "Test product 2" },3)
                },
            };

            var expected_value = cart.ItemsList.Sum(x => x.Quantity);

            var actual_value = cart.ItemsCount;

            Assert.Equal(expected_value, actual_value);
        }

        [TestMethod]
        public void CartViewModel_Class_Correct_TotalPrice()
        {
            var cart = new CartViewModel
            {
                ItemsList = new List<(ProductViewModel, int)>()
                {
                    (new ProductViewModel{Id = 1 , Name = "Test product 1", Price=10m},2),
                    (new ProductViewModel{Id = 2, Name = "Test product 2", Price = 20m },3)
                },
            };

            var expected_value = cart.ItemsList.Sum(x => x.product.Price * x.Quantity);

            var actual_value = cart.TotalPrice;

            Assert.Equal(expected_value, actual_value);
        }
    }
}
