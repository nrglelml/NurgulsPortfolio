using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
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
            _skillService.TAdd(p);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MakeActive(int id)
        {
           var value= _skillService.TGetByID(id);
            value.IsActive = true;
            _skillService.TUpdate(value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _skillService.TGetByID(id);
            value.IsActive = false;
            _skillService.TUpdate(value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditSkill(Skill p)
        {
            var skill = _skillService.TGetByID(p.Id);
            skill.SkillTitle = p.SkillTitle;
            _skillService.TUpdate(skill);
            return RedirectToAction("Index");
        }
    }
}
