using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class InterestController : BaseAdminController
    {
        private readonly IinterestService _interestService;
        private readonly IMapper _mapper;

        public InterestController(IinterestService interestService, IMapper mapper)
        {
            _interestService = interestService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var allItems = _interestService.TGetList();
            var dtoList = _mapper.Map<List<InterestAddEditDTO>>(allItems);
            return View(dtoList);
        }
        [HttpPost]
        public IActionResult AddInterest(InterestAddEditDTO p)
        {
            p.IsActive = true;
            var interest = _mapper.Map<Interest>(p);
            if (p.ImageFile != null)
            {
                interest.ImageURL = SaveImageFile(p.ImageFile);
            }
            InterestValidator validationRules = new InterestValidator();
            ValidationResult result = validationRules.Validate(interest);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Interest", new { area = "Admin" });
            }
            else
            {
                _interestService.TAdd(interest);
                return RedirectToAction("Index", "Interest", new { area = "Admin" });
            }

        }
        [HttpPost]
        public IActionResult MakeActive(int id)
        {
            var value = _interestService.TGetByID(id);
            value.IsActive = true;
            _interestService.TUpdate(value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _interestService.TGetByID(id);
            value.IsActive = false;
            _interestService.TUpdate(value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditInterest(InterestAddEditDTO p)
        {
            var interest = _mapper.Map<Interest>(p);
            if (p.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(p.ImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  p.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                interest.ImageURL = SaveImageFile(p.ImageFile);
            }
            InterestValidator validationRules = new InterestValidator();
            ValidationResult result = validationRules.Validate(interest);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Interest", new { area = "Admin" });
            }
            else
            {
                _interestService.TUpdate(interest);
                return RedirectToAction("Index", "Interest", new { area = "Admin" });
            }
        }
        [HttpPost]
        public IActionResult DeleteInterest(int id)
        {
            var value = _interestService.TGetByID(id);
            if (!string.IsNullOrEmpty(value.ImageURL))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                              value.ImageURL.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }
            _interestService.TDelete(value);
            return RedirectToAction("Index");
        }


        private string SaveImageFile(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var folder = Path.Combine(resource, "wwwroot", "interestImages");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var savelocation = Path.Combine(folder, imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/interestImages/" + imagename;
        }
    }
}
