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
        private readonly IConfiguration _configuration;

        public HomeController(IContactMeService contactMeService, IConfiguration configuration)
        {
            _contactMeService = contactMeService;
            _configuration = configuration;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactMe contactMe)
        {
            var captchaResponse = Request.Form["g-recaptcha-response"];

            if (!string.IsNullOrEmpty(captchaResponse))
            {
                var secret = _configuration["ReCaptcha:SecretKey"];

                using var httpClient = new HttpClient();
                var captchaResult = await httpClient.PostAsync(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={captchaResponse}",
                    null);
                var json = await captchaResult.Content.ReadAsStringAsync();
                var obj = System.Text.Json.JsonDocument.Parse(json);

                if (!obj.RootElement.GetProperty("success").GetBoolean())
                {
                    TempData["CaptchaError"] = "Lütfen robot olmadığınızı doğrulayın.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["CaptchaError"] = "Lütfen robot olmadığınızı doğrulayın.";
                return RedirectToAction("Index");
            }

            ContactMeValidator validator = new ContactMeValidator();
            ValidationResult validationResult = validator.Validate(contactMe);

            if (validationResult.IsValid)
            {
                _contactMeService.TAdd(contactMe);
                TempData["Success"] = "Mesajınız başarıyla gönderildi.";
                return RedirectToAction("Index");
            }

            TempData["ValidationErrors"] = string.Join("|", validationResult.Errors.Select(x => x.ErrorMessage));
            return RedirectToAction("Index");
        }
    }
}
