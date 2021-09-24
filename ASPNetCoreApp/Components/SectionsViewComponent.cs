using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApp.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
