using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApp.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
