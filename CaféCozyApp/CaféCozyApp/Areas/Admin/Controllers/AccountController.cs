using CaféCozyApp.Areas.Admin.ViewModels.Account;
using CaféCozyApp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaféCozyApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM admin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == admin.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "UserName or Password is not correct!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, admin.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not correct!");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }


        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                Fullname = "Admin",
                UserName = "admin"
            };

            var result = await _userManager.CreateAsync(admin, "Admin123_");

            if (!result.Succeeded)
            {
                return Ok(result.Errors);
            }

            return View();
        }
    }
}
