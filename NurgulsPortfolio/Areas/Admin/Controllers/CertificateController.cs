using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class CertificateController : BaseAdminController
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public IActionResult Index()
        {
            var values = _certificateService.TGetList();
            return View(values);
        }

        [HttpPost]
        public IActionResult AddCertificate(Certificate p)
        {
            p.IsActive = true;
            CertificateValidator validationRules = new CertificateValidator();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                _certificateService.TAdd(p);
                return RedirectToAction("Index", "Certificate", new { area = "Admin" });
            }
            foreach (var item in result.Errors)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return RedirectToAction("Index", "Certificate", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult EditCertificate(Certificate p)
        {
            var cert = _certificateService.TGetByID(p.Id);
            CertificateValidator validationRules = new CertificateValidator();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                cert.Issuer = p.Issuer;
                cert.Category = p.Category;
                cert.IconClass = p.IconClass;
                cert.IconBgColor = p.IconBgColor;
                cert.CredentialURL = p.CredentialURL;
                cert.CredentialId = p.CredentialId;
                cert.IssueDate = p.IssueDate;
                cert.ExpiryDate = p.ExpiryDate;
                _certificateService.TUpdate(cert);
                return RedirectToAction("Index", "Certificate", new { area = "Admin" });
            }
            foreach (var item in result.Errors)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return RedirectToAction("Index", "Certificate", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult MakeActive(int id)
        {
            var cert = _certificateService.TGetByID(id);
            cert.IsActive = true;
            _certificateService.TUpdate(cert);
            return RedirectToAction("Index", "Certificate", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var cert = _certificateService.TGetByID(id);
            cert.IsActive = false;
            _certificateService.TUpdate(cert);
            return RedirectToAction("Index", "Certificate", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult DeleteCertificate(int id)
        {
            var cert = _certificateService.TGetByID(id);
            _certificateService.TDelete(cert);
            return RedirectToAction("Index", "Certificate", new { area = "Admin" });
        }
    }
}

