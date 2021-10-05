﻿using ASPNetCoreApp.Domain.Identity;
using ASPNetCoreApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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


        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserIdentityViewModel usr)
        {
            var user = new User() { UserName = usr.Login };


            var registerResult = await userManager.CreateAsync(user, usr.Password);

            if (registerResult.Succeeded)
            {
                await signInManager.SignInAsync(user,false);

                return RedirectToAction("Index", "Home");
            }


            foreach(var item in registerResult.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(usr);
        }

        #endregion


        #region Login
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });


        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var loginResult = await signInManager.PasswordSignInAsync(model.Login,model.Password,model.RememberMe,false);


            if (loginResult.Succeeded)
                return LocalRedirect(model.ReturnUrl ?? "/home/index");

            ModelState.AddModelError("", "Ошибка аутентификации");

            return View(model);

        }
        #endregion

        public IActionResult AccessDenied() => View();

        public async Task<IActionResult> Logout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        } 
    }
}
