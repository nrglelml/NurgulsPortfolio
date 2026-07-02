using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class SettingsController : BaseAdminController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult UpdateCredentials()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCredentials(AdminCredentialsDto dto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!string.IsNullOrEmpty(dto.NewUsername) && dto.NewUsername != user.UserName)
            {
                user.UserName = dto.NewUsername;
                await _userManager.UpdateAsync(user);
            }

            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                if (dto.NewPassword != dto.ConfirmPassword)
                {
                    TempData["Error"] = "Şifreler eşleşmiyor.";
                    return RedirectToAction("UpdateCredentials");
                }

                var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (!result.Succeeded)
                {
                    TempData["Error"] = result.Errors.First().Description;
                    return RedirectToAction("UpdateCredentials");
                }
                await _signInManager.SignOutAsync();
                TempData["Success"] = "Şifre güncellendi. Lütfen yeni şifrenizle giriş yapın.";
                return RedirectToAction("SignIn", "Login", new { area = "Admin" });
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["Success"] = "Bilgiler güncellendi.";
            return RedirectToAction("UpdateCredentials");
        }
    }
}
