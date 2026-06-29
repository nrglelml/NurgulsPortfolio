using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.AdminLayout
{
    public class _TopBar:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
