using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Interests : ViewComponent
    {
        private readonly IinterestService _interestService;

        public _Interests(IinterestService interestService)
        {
            _interestService = interestService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _interestService.TGetList().Where(x => x.IsActive).ToList();
            return View(values);
        }
    }
}
