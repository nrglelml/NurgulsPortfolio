using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        private readonly UserManager<AppUser> _usermanager;
        public DashboardController(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _usermanager.FindByNameAsync(User.Identity.Name);
            ViewBag.username = values.UserName;
            return View();
        }
    }
}
