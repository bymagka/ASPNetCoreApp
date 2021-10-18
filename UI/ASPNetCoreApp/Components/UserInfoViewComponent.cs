using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Interfaces.Services;
using System.Collections.Generic;
using ASPNetCoreApp.Domain.ViewModels;
using System.Linq;

namespace ASPNetCoreApp.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity?.IsAuthenticated == true ? View("UserInfo") : View();

    }
}
