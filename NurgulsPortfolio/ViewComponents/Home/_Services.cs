using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Services:ViewComponent
    {
        private readonly IMyService _myService;
        public _Services(IMyService myService)
        {
            _myService = myService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _myService.TGetListByStatus(true);
            return View(values);
        }
    }
}
