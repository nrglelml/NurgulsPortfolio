using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class ServiceController : BaseAdminController
    {
        private readonly IMyService _myService;
        private readonly IMapper _mapper;

        public ServiceController(IMyService myService, IMapper mapper)
        {
            _myService = myService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var values = _myService.TGetList();
            var dtoList = _mapper.Map<List<ServiceAddEditDTO>>(values);
            return View(dtoList);
        }
        [HttpPost]
        public IActionResult AddService(ServiceAddEditDTO p)
        {
             p.IsActive = true;
             var service = _mapper.Map<Service>(p);
            if (p.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(p.ImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  p.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                service.ImageURL = SaveImageFile(p.ImageFile);
            }

            ServiceValidator validationRules=new ServiceValidator();
             ValidationResult result = validationRules.Validate(service);

            if (result.IsValid)
            {
                _myService.TAdd(service);
                return RedirectToAction("Index", "Service", new { area = "Admin" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Service", new { area = "Admin" });
            }
        }
        [HttpPost]
        public IActionResult MakeActive(int id)
        {
            var value = _myService.TGetByID(id);
            value.IsActive = true;
            _myService.TUpdate(value);
            return RedirectToAction("Index", "Service", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _myService.TGetByID(id);
            value.IsActive = false;
            _myService.TUpdate(value);
            return RedirectToAction("Index", "Service", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult EditService(ServiceAddEditDTO p)
        {
            var service=_mapper.Map<Service>(p);
            if (p.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(p.ImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  p.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                service.ImageURL = SaveImageFile(p.ImageFile);
            }
            else
            {
                service.ImageURL = p.ImageURL;
            }

            ServiceValidator validationRules = new ServiceValidator();
            ValidationResult result = validationRules.Validate(service);

            if (result.IsValid)
            {
                _myService.TUpdate(service);
                return RedirectToAction("Index", "Service", new { area = "Admin" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Service", new { area = "Admin" });
            }
        
        }
        [HttpPost]
        public IActionResult DeleteService(int id)
        {
           var value= _myService.TGetByID(id);
            _myService.TDelete(value);
            return RedirectToAction("Index", "Service", new { area = "Admin" });
        }

        private string SaveImageFile(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var folder = Path.Combine(resource, "wwwroot", "serviceImages");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var savelocation = Path.Combine(folder, imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/serviceImages/" + imagename;  
        }
    }
}
