using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _About: ViewComponent
    {
        private readonly IAboutMeService _aboutMeService;
        private readonly ISkillService _skillService;

        public _About(IAboutMeService aboutMeService, ISkillService skillService)
        {
            _aboutMeService = aboutMeService;
            _skillService = skillService;
        }

        public IViewComponentResult Invoke()
        {
            var about = _aboutMeService.TGetList().FirstOrDefault();
            ViewBag.Skills = _skillService.TGetList().Where(x => x.IsActive).ToList();
            return View(about);
        }
    }
}
