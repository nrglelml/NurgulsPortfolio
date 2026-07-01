using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.AdminDashboard
{
    public class _CardsCount:ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.projects = c.Projects.Count();
            ViewBag.projectsT = c.Projects.Where(x => x.IsActive == true).Count();
            ViewBag.messages = c.ContactMes.Count();
            ViewBag.skills = c.Skills.Count();
            ViewBag.skillsT = c.Skills.Where(x => x.IsActive == true).Count();
            ViewBag.cv = c.CvFiles.Count();
            return View();
        }
    }
}
