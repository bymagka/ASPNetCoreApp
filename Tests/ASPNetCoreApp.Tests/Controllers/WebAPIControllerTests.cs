using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Xunit.Assert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ASPNetCoreApp.Interfaces.TestApi;
using ASPNetCoreApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApp.Tests.Controllers
{
    [TestClass]
    public class WebAPIControllerTests
    {
        
        [TestMethod]
        public void Index_Returns_Views_WithValues()
        {
            var data = Enumerable.Range(1, 10)
                .Select(x => $"Value is {x}")
                .ToArray();

            Mock<IValuesService> values_service_mock = new Mock<IValuesService>();

            values_service_mock.Setup(f => f.GetAll())
                .Returns(data);

            WebAPIController controller = new WebAPIController(values_service_mock.Object);

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);

            var data_result = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            int i = 0;

            foreach(var item in data_result)
            {
                string expected_value = data[i++];
                Assert.Equal(expected_value, item);
            }
        }
    }
}
