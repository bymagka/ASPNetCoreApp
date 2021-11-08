using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Assert = Xunit.Assert;
using Moq;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Services.Services;

namespace ASPNetCoreApp.Services.Tests.Services
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _Cart;

        private Mock<ICartStore> cartStoreMock;

        private Mock<IProductData> cartProductData;

        private ICartService cartService;

        [TestInitialize]
        public void TestInitialize()
        {
            _Cart = new Cart()
            {
                CartItems = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity= 1},
                    new CartItem(){ProductId= 2, Quantity = 3}

                }
            };

            var products = new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 1.1m,
                    Order = 1,
                    ImageUrl = "img_1.png",
                    Brand = new Brand
                    {
                        Id = 1,
                        Name = "Brand 1",
                        Order = 1,
                    },
                    SectionId = 1,
                    Section = new Section
                    {
                        Id = 1,
                        Name = "Section 1",
                        Order = 1,
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 1.45m,
                    Order = 2,
                    ImageUrl = "img_2.png",
                    Brand = new Brand
                    {
                        Id = 2,
                        Name = "Brand 2",
                        Order = 2,
                    },
                    SectionId = 2,
                    Section = new Section
                    {
                        Id = 2,
                        Name = "Section 2",
                        Order = 2,
                    }
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Price = 2.1m,
                    Order = 3,
                    ImageUrl = "img_3.png",
                    Brand = new Brand
                    {
                        Id = 3,
                        Name = "Brand 3",
                        Order = 3,
                    },
                    SectionId = 3,
                    Section = new Section
                    {
                        Id = 3,
                        Name = "Section 31",
                        Order = 3,
                    }
                },
            };


            cartStoreMock = new Mock<ICartStore>();
            cartStoreMock.Setup(x => x.Cart).Returns(_Cart);

            cartProductData = new Mock<IProductData>();
            cartProductData.Setup(x => x.GetProducts(It.IsAny<ProductFilter>())).Returns(new ProductsPage(products,products.Length));

            cartService = new CartService(cartStoreMock.Object, cartProductData.Object);
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


        [TestMethod]
        public void CartService_Add_WorkCorrect()
        {
            _Cart.CartItems.Clear();

            const int expected_id = 5;
            const int expected_items_count = 1;

            cartService.Add(expected_id);

            var actual_items_count = _Cart.ItemsCount;

            Assert.Equal(expected_items_count, actual_items_count);

            Assert.Single(_Cart.CartItems);

            Assert.Equal(expected_id, _Cart.CartItems.Single().ProductId);
        }

        [TestMethod]
        public void CartService_Remove_CorrectItem()
        {
            const int item_id = 1;

            const int expected_product_id = 2;

            cartService.Remove(item_id);

            Assert.Single(_Cart.CartItems);

            Assert.Equal(expected_product_id, _Cart.CartItems.Single().ProductId);

        }


        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            const int item_id = 2;
            const int expected_quantity = 2;
            const int expected_items_count = 3;
            const int expected_products_count = 2;

            cartService.Decrement(item_id);

            Assert.Equal(expected_items_count, _Cart.ItemsCount);

            Assert.Equal(expected_products_count,_Cart.CartItems.Count);

            var items = _Cart.CartItems.ToArray();

            Assert.Equal(expected_quantity, items[1].Quantity);

            Assert.Equal(item_id, items[1].ProductId);
        }


        [TestMethod]
        public void CartService_Decrement_RemoveItem_When_Decrement_To_0()
        {
            const int item_id = 1;
            const int expected_items_count = 3;
            

            cartService.Decrement(item_id);

            Assert.Equal(expected_items_count, _Cart.ItemsCount);
            Assert.Single(_Cart.CartItems);
            
        }

        [TestMethod]
        public void CartService_Clear_Correct()
        {
            cartService.Clear();

            Assert.Empty(_Cart.CartItems);
        }

        [TestMethod]
        public void CartService_GetViewModel_Work_Correct()
        {
            const int expected_items_count = 4;
            const decimal expected_first_item_price = 1.1m;
            const decimal expected_total_price = 5.45m;

            CartViewModel result = cartService.GetViewModel();

            Assert.Equal(expected_items_count, result.ItemsCount);
            Assert.Equal(expected_first_item_price, result.ItemsList.First().product.Price);
            Assert.Equal(expected_total_price, result.TotalPrice);
        }

    }
}
