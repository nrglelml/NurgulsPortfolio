using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseAdminController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> SignIn(AdminLoginDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
        dto.UserName,
        dto.Password,
        isPersistent: false,
        lockoutOnFailure: false
    );

            if (result.Succeeded)
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View(dto);
        }
        
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login", new { area = "Admin" });
        }
        
    }
}
