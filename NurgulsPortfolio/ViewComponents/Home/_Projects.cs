using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Projects:ViewComponent
    {
        private readonly IProjectService _projectService;
        public _Projects(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _projectService.TGetListWithSkill().Where(x=>x.IsActive==true).ToList();
            return View(values);
        }
    }
}
