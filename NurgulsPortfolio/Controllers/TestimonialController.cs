using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }
        [Route("/referansOlustur")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTestimonial(Testimonials p)
        {
            TestimonialValidator validationRules = new TestimonialValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                _testimonialService.TAdd(p);
                TempData["Success"] = "Yorumunuz başarıyla gönderildi.";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                TempData["ValidationErrors"] = string.Join("|", result.Errors.Select(x => x.ErrorMessage));
                return RedirectToAction("Index");
            }
        }
    }
}
