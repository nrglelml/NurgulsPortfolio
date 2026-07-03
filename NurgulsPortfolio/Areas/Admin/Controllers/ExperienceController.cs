using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class ExperienceController :BaseAdminController
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 4;

            var allItems = _experienceService.TGetList();
            int totalCount = allItems.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedSkills = allItems
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new PagedResultDTO<Experience>
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
        public IActionResult AddExperience(Experience p)
        {
            p.IsActive = true;
            ExperienceValidator validationRules=new ExperienceValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                _experienceService.TAdd(p);
                return RedirectToAction("Index", "Experience", new { area = "Admin" });
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
            var value = _experienceService.TGetByID(id);
            value.IsActive = true;
            _experienceService.TUpdate(value);
            return RedirectToAction("Index", "Experience", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _experienceService.TGetByID(id);
            value.IsActive = false;
            _experienceService.TUpdate(value);
            return RedirectToAction("Index", "Experience", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult EditExperience(Experience p)
        {
            var experience = _experienceService.TGetByID(p.Id);

            ExperienceValidator validationRules = new ExperienceValidator();
            ValidationResult result = validationRules.Validate(p);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                return RedirectToAction("Index", "Experience", new { area = "Admin" });
            }

            experience.Company = p.Company;
            experience.Position = p.Position;
            experience.Location = p.Location;
            experience.StartDate = p.StartDate;
            experience.EndDate = p.EndDate;    
            experience.Description = p.Description; 

            _experienceService.TUpdate(experience);
            return RedirectToAction("Index", "Experience", new { area = "Admin" });
        }
    }
}
