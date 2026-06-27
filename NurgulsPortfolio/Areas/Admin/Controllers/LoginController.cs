using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{

    public class LoginController : BaseAdminController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }
        //[HttpPost]
        //public async Task<IActionResult> UpdateCredentials(AdminCredentialsDto dto)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (!string.IsNullOrEmpty(dto.NewUsername))
        //    {
        //        user.UserName = dto.NewUsername;
        //        await _userManager.UpdateAsync(user);
        //    }

        //    if (!string.IsNullOrEmpty(dto.NewPassword))
        //    {
        //        if (dto.NewPassword != dto.ConfirmPassword)
        //        {
        //            TempData["Error"] = "Şifreler eşleşmiyor.";
        //            return RedirectToAction("Index");
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        //        if (!result.Succeeded)
        //        {
        //            TempData["Error"] = result.Errors.First().Description;
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    await _signInManager.RefreshSignInAsync(user);
        //    TempData["Success"] = "Bilgiler güncellendi.";
        //    return RedirectToAction("Index");
        //}
    }
}
