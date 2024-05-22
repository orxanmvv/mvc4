using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Models;
using WebApplication14.ViewModels.Account;

namespace WebApplication14.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user1 = new User()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                Surname = registerVm.Surname,
                UserName = registerVm.Email
            };
            var result = await _userManager.CreateAsync(user1, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
                return View();
            }
            return RedirectToAction("Login");

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid) 
            { 
                return View(loginVm); 
            }
            User user2;
            if (loginVm.UsernameOrEmail.Contains("@"))
            {
                user2 = await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail);
            }
            else 
            { 
                user2 = await _userManager.FindByNameAsync(loginVm.UsernameOrEmail); 
            }
            if (user2 == null)
            {
                ModelState.AddModelError("", "usernameoremail ve ya password yanlis daxil edilib");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user2, loginVm.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "birazdan yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "usernameoremail ve ya password yanlisdir");
                return View();

            }
            await _signInManager.SignInAsync(user2, loginVm.RememberMe);
            
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
