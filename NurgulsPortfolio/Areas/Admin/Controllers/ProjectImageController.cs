using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class ProjectImageController :BaseAdminController
    {
        private readonly IProjectImageService _projectImageService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectImageController(IProjectImageService projectImageService, IProjectService projectService, IMapper mapper)
        {
            _projectImageService = projectImageService;
            _projectService = projectService;
            _mapper = mapper;
        }

        public IActionResult Index(int id)
        {
            var project = _projectService.TGetByID(id);
            var projectImages = _projectImageService.TGetListByProjectId(id);
            var dtoList = _mapper.Map<List<ProjectImageAddEditDTO>>(projectImages);

            ViewBag.ProjectTitle = project?.ProjectTitle;
            ViewBag.ProjectId = id;

            return View(dtoList);
        }
        [HttpPost]
        public IActionResult AddImage(ProjectImageAddEditDTO p)
        {
            p.IsActive = true;
            var image = _mapper.Map<ProjectImage>(p);
            if (p.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(p.ImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  p.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                image.ImageURL = SaveImageFile(p.ImageFile);
            }
           _projectImageService.TAdd(image);
            return RedirectToAction("Index", new { id = p.ProjectId });


        }
        [HttpPost]
        public IActionResult MakeActive(int id, int projectId)
        { 
            var value = _projectImageService.TGetByID(id);
            value.IsActive = true;
            _projectImageService.TUpdate(value);
            return RedirectToAction("Index", new { id = projectId });
        }
        [HttpPost]
        public IActionResult MakePassive(int id, int projectId)
        {
            var value = _projectImageService.TGetByID(id);
            value.IsActive = false;
            _projectImageService.TUpdate(value);
            return RedirectToAction("Index", new { id = projectId });
        }
        [HttpPost]
        public IActionResult DeleteImage(int id, int projectId)
        {
            var value = _projectImageService.TGetByID(id);
            if (!string.IsNullOrEmpty(value.ImageURL))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                              value.ImageURL.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }
            _projectImageService.TDelete(value);
            return RedirectToAction("Index", new { id = projectId });
        }
        private string SaveImageFile(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var folder = Path.Combine(resource, "wwwroot", "projectImages");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var savelocation = Path.Combine(folder, imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/projectImages/" + imagename;
        }

    }
}
