using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Contact: ViewComponent
    {
 
        private readonly IContactService _contactService;
        private readonly ISocialMediaService _socialMediaService;
        public _Contact(IContactService contactService, ISocialMediaService socialMediaService)
        {
            _contactService = contactService;
            _socialMediaService = socialMediaService;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.social = _socialMediaService.TGetList();
            var values = _contactService.TGetList().FirstOrDefault();
            ViewBag.ContactMe = new ContactMe();
            return View(values);
        }
    }
}
