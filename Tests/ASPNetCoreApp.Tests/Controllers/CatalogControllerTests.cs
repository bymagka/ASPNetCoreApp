using ASPNetCoreApp.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASPNetCoreApp.Controllers;
using Moq;
using ASPNetCoreApp.Domain.Entities;

using Assert = Xunit.Assert;
using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Domain;
using Microsoft.Extensions.Configuration;

namespace ASPNetCoreApp.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returns_With_Correct_View()
        {
            const int expected_id = 321;
            const string expected_name = "Test ";
            const int expected_order = 5;
            const decimal expected_price = 13.5m;
            const string expected_img_url = "/img/product.img";

            const int expected_brand_id = 7;
            const string expected_brand_name = "Test brand";
            const int expected_brand_order = 10;

            const int expected_section_id = 14;
            const string expected_section_name = "Test section";
            const int expected_section_order = 123;

            var product_service_mock = new Mock<IProductData>();

            product_service_mock
                .Setup(i => i.GetProductById(It.Is<int>(id => id > 0)))
                .Returns<int>(id=> new Product 
                { 
                    Id = id,
                    Name = expected_name,
                    Order = expected_order,
                    Price = expected_price,
                    ImageUrl = expected_img_url,
                    
                    BrandId = expected_brand_id,
                    Brand = new Brand
                    {
                        Id = expected_brand_id,
                        Name = expected_brand_name,
                        Order = expected_brand_order,
                    },

                    SectionId = expected_section_id,
                    Section = new Section
                    {
                        Id = expected_section_id,
                        Name = expected_section_name,
                        Order = expected_section_order,
                    }                   
                });

            var configuration_mock = new Mock<IConfiguration>();

            configuration_mock.Setup(c => c["CatalogPageSize"]).Returns("4");

            var controller = new CatalogController(product_service_mock.Object,configuration_mock.Object);

            var result = controller.Details(expected_id);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_id, model.Id);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_price,model.Price);
            Assert.Equal(expected_img_url, model.ImageUrl);
            Assert.Equal(expected_brand_name,model.Brand);
            Assert.Equal(expected_section_name, model.Section);

            product_service_mock.Verify(g => g.GetProductById(It.Is<int>(id => id > 0)));
            product_service_mock.VerifyAll();
        }

        [TestMethod]
        public void Details_Unfind_Product_Returns_NotFound()
        {
            const int expected_id = 321;

            var product_service_mock = new Mock<IProductData>();

            product_service_mock
                .Setup(i => i.GetProductById(It.Is<int>(id => id > 0)))
                .Returns<int>(id => null);

            var configuration_mock = new Mock<IConfiguration>();

            configuration_mock.Setup(c => c["CatalogPageSize"]).Returns("4");

            var controller = new CatalogController(product_service_mock.Object,configuration_mock.Object);

            var result = controller.Details(expected_id);

            var view_result = Assert.IsType<NotFoundResult>(result);


            product_service_mock.Verify(g => g.GetProductById(It.Is<int>(id => id > 0)));
            product_service_mock.VerifyAll();
        }

    }
}
