using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Hero : ViewComponent
    {
        private readonly IAboutMeService _aboutMeService;
        public _Hero(IAboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }
        public IViewComponentResult Invoke()
        {
            var values= _aboutMeService.TGetList().FirstOrDefault();
            return View(values);
        }
    }
}
