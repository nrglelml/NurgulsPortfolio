using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class EducationController : BaseAdminController
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 4;

            var allItems = _educationService.TGetList();
            int totalCount = allItems.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedSkills = allItems
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResultDTO<Education>
            {
                Items = pagedSkills,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(result);
        }
        [HttpPost]
        public IActionResult AddEducation(Education p)
        {
            p.IsActive = true;
            EducationValidator validationRules = new EducationValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                _educationService.TAdd(p);
                return RedirectToAction("Index", "Education", new { area = "Admin" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(p);
            }
        }
        [HttpPost]
        public IActionResult MakeActive(int id)
        {
            var value = _educationService.TGetByID(id);
            value.IsActive = true;
            _educationService.TUpdate(value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _educationService.TGetByID(id);
            value.IsActive = false;
            _educationService.TUpdate(value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditEducation(Education p)
        {
            var education = _educationService.TGetByID(p.Id);

            EducationValidator validationRules = new EducationValidator();
            ValidationResult result = validationRules.Validate(p);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                return RedirectToAction("Index");
            }

            education.School = p.School;
            education.Department = p.Department;
            education.Degree=p.Degree;
            education.GPA = p.GPA;
            education.StartDate = p.StartDate;
            education.EndDate = p.EndDate;
            _educationService.TUpdate(education);
            
            return RedirectToAction("Index");
        }
    }
}
