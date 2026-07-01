using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NurgulsPortfolio.ViewComponents.AdminDashboard
{
    public class _LatestProject:ViewComponent
    {
        Context c= new Context();
        public IViewComponentResult Invoke()
        {
            var value = c.Projects.Include(x => x.Skill).OrderByDescending(x => x.Id).FirstOrDefault();
            return View(value);
        }
    }
}
