using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.AdminLayout
{
    public class _TopBar:ViewComponent
    {
        private readonly IAboutMeService _aboutMeService;

        public _TopBar(IAboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }

        public IViewComponentResult Invoke()
        {
            var aboutMe = _aboutMeService.TGetList().FirstOrDefault();
            return View(aboutMe);
        }
    }
}
