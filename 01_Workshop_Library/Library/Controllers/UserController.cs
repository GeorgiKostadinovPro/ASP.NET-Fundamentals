using Library.Data.Models;
using Library.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("All", "Books");
            }

            RegisterViewModel registerViewModel = new RegisterViewModel();

            return this.View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            ApplicationUser user = await this.userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                this.ModelState.AddModelError(string.Empty, "User with this email already exists!");

                return this.View(model);
            }

            user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var registerResult = await this.userManager.CreateAsync(user, model.Password);

            if (registerResult.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);

                //return this.RedirectToAction("Login", "User");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registerResult.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("All", "Books");
            }

            LoginViewModel loginViewModel = new LoginViewModel()
            {
                //ReturnUrl = returnUrl
            };

            return this.View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            ApplicationUser userToLogin = await this.userManager
                .FindByNameAsync(model.UserName);

            if (userToLogin != null)
            {
                var loginResult = await this.signInManager
                .PasswordSignInAsync(userToLogin, model.Password, isPersistent: false, lockoutOnFailure: false);
               
                if (loginResult.Succeeded)
                {
                    //if (model.ReturnUrl != null)
                    //{
                    //    return this.Redirect(model.ReturnUrl);
                    //}
                    
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login attempt!");
            
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
