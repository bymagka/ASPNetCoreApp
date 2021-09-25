using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Services.Interfaces;

namespace ASPNetCoreApp.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }


        public IViewComponentResult Invoke() => View(_ProductData.GetSections());
    }
}
