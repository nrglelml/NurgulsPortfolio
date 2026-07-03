using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class AboutController : BaseAdminController
    {
        private readonly IAboutMeService _aboutMeService;
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public AboutController(IAboutMeService aboutMeService, IContactService contactService, IMapper mapper)
        {
            _aboutMeService = aboutMeService;
            _contactService = contactService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var aboutMe = _aboutMeService.TGetList().FirstOrDefault();
            var contact = _contactService.TGetList().FirstOrDefault();

            var vm = new AboutPageViewDTO
            {
                AboutMe = aboutMe != null ? _mapper.Map<AboutMeAddEditDTO>(aboutMe) : new AboutMeAddEditDTO(),
                Contact = contact ?? new Contact()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(AboutPageViewDTO dto)
        {
            var aboutMe = _mapper.Map<AboutMe>(dto.AboutMe);
            if (dto.AboutMe.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(dto.AboutMe.ImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                  dto.AboutMe.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                aboutMe.ImageURL = SaveImageFile(dto.AboutMe.ImageFile);
            }
            else
            {
                aboutMe.ImageURL = dto.AboutMe.ImageURL;
            }

            if (dto.AboutMe.Id == 0)
                _aboutMeService.TAdd(aboutMe);
            else
                _aboutMeService.TUpdate(aboutMe);

            if (dto.Contact.Id == 0)
                _contactService.TAdd(dto.Contact);
            else
                _contactService.TUpdate(dto.Contact);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }


        private string SaveImageFile(IFormFile imageFile)
        {
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            var imagename = Guid.NewGuid() + extension;
            var folder = Path.Combine(resource, "wwwroot", "aboutMeImages");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var savelocation = Path.Combine(folder, imagename);

            using (var stream = new FileStream(savelocation, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/aboutMeImages/" + imagename;
        }
    }
}
