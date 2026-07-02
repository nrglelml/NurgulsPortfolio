using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NurgulsPortfolio.Controllers
{
    [AllowAnonymous]
    public class ErrorPageController : Controller
    {

        public IActionResult Error404(int code)
        {
            return View();
        }

    }
}
