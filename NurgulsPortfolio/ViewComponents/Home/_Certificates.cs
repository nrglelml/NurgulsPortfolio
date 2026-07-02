using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.ViewComponents.Home
{
    public class _Certificates : ViewComponent
    {
        private readonly ICertificateService _certificateService;

        public _Certificates(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _certificateService.TGetList().Where(x => x.IsActive).ToList();
            return View(values);
        }
    }
}
