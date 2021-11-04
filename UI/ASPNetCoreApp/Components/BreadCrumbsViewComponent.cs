using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public BreadCrumbsViewComponent(IProductData productData)
        {
            this.productData = productData;
        }

        public IViewComponentResult Invoke()
        {
            BreadCrumbsViewModel model = new BreadCrumbsViewModel();

            if (int.TryParse(Request.Query["SectionId"],out int section_id))
            {
                model.Section = productData.GetSectionById(section_id);

                if (model.Section.ParentId is { } parent_section_id) model.Section.Parent = productData.GetSectionById(parent_section_id);

            }

            if (int.TryParse(Request.Query["BrandId"], out int brand_id))
            {
                model.Brand = productData.GetBrandById(brand_id);
            }

            if (int.TryParse(ViewContext.RouteData.Values["id"]?.ToString(), out int product_id)) model.Product = productData.GetProductById(product_id)?.Name;

            return View(model);
        }
    }
}
