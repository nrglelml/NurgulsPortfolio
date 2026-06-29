using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace NurgulsPortfolio.ViewComponents.AdminDashboard
{
    public class _RecentMessages:ViewComponent
    {
       private readonly IContactMeService _contactMeService;

        public _RecentMessages(IContactMeService contactMeService)
        {
            _contactMeService = contactMeService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _contactMeService.TGetList().Take(4).ToList();
            return View(values);
        }
    }
}
