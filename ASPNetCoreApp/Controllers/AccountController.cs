using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ASPNetCoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Register
        public IActionResult Register() => View(new UserIdentityViewModel());


        [HttpPost]
        public IActionResult Register(UserIdentityViewModel usr) => RedirectToAction("Home", "Index");
        #endregion



        public IActionResult Login() => View();

        

       

        public IActionResult AccessDenied() => View();

        public IActionResult Logout() => RedirectToAction("Index","Home");
    }
}
