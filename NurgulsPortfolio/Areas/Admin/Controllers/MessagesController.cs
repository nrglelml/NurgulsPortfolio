using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class MessagesController : BaseAdminController
    {
        private readonly IContactMeService _contactMeService;

        public MessagesController(IContactMeService contactMeService)
        {
            _contactMeService = contactMeService;
        }

        public IActionResult Index()
        {
            var values= _contactMeService.TGetList();
            var c = new Context();
            ViewBag.messages = c.ContactMes.Count();
            return View(values);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var value = _contactMeService.TGetByID(id);
            _contactMeService.TDelete(value);
            return RedirectToAction("Index", "Messages", new { area = "Admin" });
       
        }
    }
}
