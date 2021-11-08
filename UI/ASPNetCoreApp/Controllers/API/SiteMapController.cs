using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvcSitemap;
    
namespace ASPNetCoreApp.Controllers.AdfgPI
{
    public class SiteMapController : ControllerBase 
    {
        public IActionResult Index([FromServices] IProductData productData)
        {
            List<SitemapNode> nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index","Home")),
                new SitemapNode(Url.Action("ContactUst","Home")),
                new SitemapNode(Url.Action("Index","Catalog")),
                new SitemapNode(Url.Action("Index","WebAPI")),
            };

            nodes.AddRange(productData.GetSections().Select(x => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = x.Id }))));

            foreach(var brand in productData.GetBrands())
            {
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));
            }

            foreach (var product in productData.GetProducts().Products)
            {
                nodes.Add(new SitemapNode(Url.Action("Details", "Catalog", new { product.Id })));
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
