using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Experience : ViewComponent
    {
        private readonly IEducationService _educationService;
        private readonly IExperienceService _experienceService;

        public _Experience(IEducationService educationService, IExperienceService experienceService)
        {
            _educationService = educationService;
            _experienceService = experienceService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Education = _educationService.TGetListByStatus(true).OrderByDescending(x => x.StartDate).ToList();
            var values = _experienceService.TGetListByStatus(true).OrderByDescending(x => x.StartDate).ToList();
            return View(values);
        }
    }
}
