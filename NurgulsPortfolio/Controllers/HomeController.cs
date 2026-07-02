using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NurgulsPortfolio.Models;
using System.Diagnostics;

namespace NurgulsPortfolio.Controllers
{
    public class HomeController : BaseUIController
    {
        private readonly IContactMeService _contactMeService;

        public HomeController(IContactMeService contactMeService)
        {
            _contactMeService = contactMeService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(ContactMe contactMe)
        {
            ContactMeValidator validator = new ContactMeValidator();
            ValidationResult result = validator.Validate(contactMe);

            if (result.IsValid)
            {
                _contactMeService.TAdd(contactMe);
                TempData["Success"] = "Mesajýnýz baþarýyla gönderildi.";
                return RedirectToAction("Index");
            }

            TempData["ValidationErrors"] = string.Join("|", result.Errors.Select(x => x.ErrorMessage));
            ViewData["ContactForm"] = contactMe;
            return RedirectToAction("Index");
        }
    }
}
