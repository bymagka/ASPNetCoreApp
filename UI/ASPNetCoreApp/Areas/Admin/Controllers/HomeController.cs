using ASPNetCoreApp.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Areas.Admin.Controllers
{
    [Authorize(Roles = Role.Administrators)]
    [Area("Admin")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
