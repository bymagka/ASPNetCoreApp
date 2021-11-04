using AngleSharp.Html.Parser;
using ASPNetCore.WebAPI;
using ASPNetCoreApp.Services.Infostructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Assert = Xunit.Assert;

namespace ASPNetCoreApp.WebApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTests
    {
        private readonly WebApplicationFactory<Startup> host = new WebApplicationFactory<Startup>();

        [TestMethod]
        public async Task GetValues_IntegrityTest()
        {
            var client = host.CreateClient();

            var response = await client.GetAsync(WebApiAdresses.Values);

            response.EnsureSuccessStatusCode();

            var values = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();

            Assert.Equal(10, values.Count());

            //var parser = new HtmlParser();

            //var html = await parser.ParseDocumentAsync(await response.Content.ReadAsStreamAsync());
        }
    }
}
