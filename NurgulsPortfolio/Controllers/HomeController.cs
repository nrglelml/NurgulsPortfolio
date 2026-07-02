using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NurgulsPortfolio.Models;

namespace NurgulsPortfolio.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
