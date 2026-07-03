using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class ProjectController : BaseAdminController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly ISkillService _skillService;


        public ProjectController(IProjectService projectService, IMapper mapper, ISkillService skillService)
        {
            _projectService = projectService;
            _mapper = mapper;
            _skillService = skillService;
        }

        public IActionResult Index()
        {
            var allItems = _projectService.TGetListWithSkill();
            var dtoList = _mapper.Map<List<ProjectAddEditDTO>>(allItems);
            dtoList.ForEach(x => x.SkillTitle = allItems
       .FirstOrDefault(s => s.Id == x.Id)?.Skill?.SkillTitle);

            ViewBag.SkillList = _skillService.TGetList()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SkillTitle
                }).ToList();

            return View(dtoList);

        }
        [HttpPost]
        public IActionResult AddProject(ProjectAddEditDTO p)
        {
            p.IsActive = true;
            var project = _mapper.Map<Project>(p);
            if (p.CoverImageFile != null)
            {
                if (!string.IsNullOrEmpty(p.CoverImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  p.CoverImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                project.CoverImageURL = SaveImageFile(p.CoverImageFile);
            }
            ProjectValidator validationRules = new ProjectValidator();
            ValidationResult result = validationRules.Validate(project);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Project", new { area = "Admin" });
            }
            else
            {
                _projectService.TAdd(project);
                return RedirectToAction("Index", "Project", new { area = "Admin" });
            }

        }
        [HttpPost]
        public IActionResult MakeActive(int id)
        {
            var value = _projectService.TGetByID(id);
            value.IsActive = true;
            _projectService.TUpdate(value);
            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult MakePassive(int id)
        {
            var value = _projectService.TGetByID(id);
            value.IsActive = false;
            _projectService.TUpdate(value);
            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }
        [HttpPost]
        public IActionResult EditProject(ProjectAddEditDTO p)
        {
            var project = _mapper.Map<Project>(p);
            if (p.CoverImageFile != null)
            {
                if (!string.IsNullOrEmpty(p.CoverImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  p.CoverImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                project.CoverImageURL = SaveImageFile(p.CoverImageFile);
            }
            ProjectValidator validationRules = new ProjectValidator();
            ValidationResult result = validationRules.Validate(project);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return RedirectToAction("Index", "Project", new { area = "Admin" });
            }
            else
            {
                _projectService.TUpdate(project);
                return RedirectToAction("Index", "Project", new { area = "Admin" });
            }
        }
        [HttpPost]
        public IActionResult DeleteProject(int id)
        {
            var value = _projectService.TGetByID(id);
            if (!string.IsNullOrEmpty(value.CoverImageURL))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                              value.CoverImageURL.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }
            _projectService.TDelete(value);
            return RedirectToAction("Index", "Project", new { area = "Admin" });
        }


        private string SaveImageFile(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var folder = Path.Combine(resource, "wwwroot", "projectCoverImages");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var savelocation = Path.Combine(folder, imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/projectCoverImages/" + imagename;
        }
    }
}
