using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Controllers
{
    public class ProjectController : BaseUIController
    {
        private readonly IProjectService _projectService;
        private readonly IProjectImageService _projectImageService;

        public ProjectController(IProjectService projectService, IProjectImageService projectImageService)
        {
            _projectService = projectService;
            _projectImageService = projectImageService;
        }

        [Route("/projeler")]
        public IActionResult Index()
        {
            var projects = _projectService.TGetListWithSkill()
         .Where(x => x.IsActive)
         .ToList();
            return View(projects);
        }
        [Route("/projeler/{id}")]
        public IActionResult ProjectDetail(int id)
        {
            var project = _projectService.TGetListWithSkill()
                .FirstOrDefault(x => x.Id == id);

            if (project == null) return NotFound();

            var images = _projectImageService.TGetListByProjectId(id)
                .Where(x => x.IsActive)
                .ToList();

            ViewBag.Images = images;
            return View(project);
        }
    }
}
