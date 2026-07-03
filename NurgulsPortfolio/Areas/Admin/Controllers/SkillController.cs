using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class SkillController : BaseAdminController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 4; 

            var allSkills = _skillService.TGetList(); 
            int totalCount = allSkills.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedSkills = allSkills
                .Skip((page - 1) * pageSize) 
                .Take(pageSize)              
                .ToList();

            var result = new PagedResultDTO<Skill>
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
        public IActionResult AddSkill(Skill p)
        {
            p.IsActive = true;
            SkillValidator validationRules = new SkillValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                _skillService.TAdd(p);
                return RedirectToAction("Index", "Skill", new { area = "Admin" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Skill", new { area = "Admin" });
            }
            
        }
        [HttpPost]
        public IActionResult MakeActive(int id)
        {
           var value= _skillService.TGetByID(id);
            value.IsActive = true;
            _skillService.TUpdate(value);
            return RedirectToAction("Index", "Skill", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _skillService.TGetByID(id);
            value.IsActive = false;
            _skillService.TUpdate(value);
            return RedirectToAction("Index", "Skill", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult EditSkill(Skill p)
        {
            var skill = _skillService.TGetByID(p.Id);
            SkillValidator validationRules = new SkillValidator();
            ValidationResult result = validationRules.Validate(p);

            if (result.IsValid)
            {
                skill.SkillTitle = p.SkillTitle;
                _skillService.TUpdate(skill);
                return RedirectToAction("Index", "Skill", new { area = "Admin" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Skill", new { area = "Admin" });
            }
        }
    }
}
