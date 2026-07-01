using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class SocialMediaController : BaseAdminController
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public IActionResult Index()
        {
            var values = _socialMediaService.TGetList();
            return View(values);
        }
        [HttpPost]
        public IActionResult AddSocialMedia(SocialMedia p)
        {
            SocialMediaValidator validationRules = new SocialMediaValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                _socialMediaService.TAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult EditSocialMedia(SocialMedia p)
        {
            var socialMedia = _socialMediaService.TGetByID(p.Id);
            SocialMediaValidator validationRules = new SocialMediaValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                socialMedia.MediaName = p.MediaName;
                socialMedia.MediaURL = p.MediaURL;
                _socialMediaService.TUpdate(socialMedia);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult DeleteSocialMedia(int id)
        {
            var values = _socialMediaService.TGetByID(id);
            _socialMediaService.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}
