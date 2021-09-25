using ASPNetCoreApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApp.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData productData)
        {
            _ProductData = productData;
        }

        public IViewComponentResult Invoke() => View(_ProductData.GetBrands());
    }
}
