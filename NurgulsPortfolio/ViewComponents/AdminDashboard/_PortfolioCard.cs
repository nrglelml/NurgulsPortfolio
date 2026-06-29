using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.AdminDashboard
{
    public class _PortfolioCard:ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
