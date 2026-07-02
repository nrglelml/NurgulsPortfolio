using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Areas.Admin.Controllers
{
    public class CvFileController : BaseAdminController
    {
        private readonly ICvFileService _cvFileService;
        private readonly IMapper _mapper;

        public CvFileController(ICvFileService cvFileService, IMapper mapper)
        {
            _cvFileService = cvFileService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var values = _cvFileService.TGetList();
            var dtoList = _mapper.Map<List<CvFileAddEditDTO>>(values);
            return View(dtoList);
        }

        [HttpPost]
        public IActionResult Upload(CvFileAddEditDTO p)
        {
            if (p.File != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.File.FileName).ToLower();
                var fileName = Guid.NewGuid() + extension;
                var folder = Path.Combine(resource, "wwwroot", "cvFiles");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var savePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    p.File.CopyTo(stream);
                }
                var cvFile = new CvFile
                {
                    Title = p.Title,
                    FileName = fileName,
                    FileSizeBytes = p.File.Length,
                    UploadedAt = DateTime.Now,
                    IsLatest = p.IsLatest,
                    DisplayOrder = p.DisplayOrder
                };

                _cvFileService.TAdd(cvFile);
            }

            return RedirectToAction("Index", "CvFile", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult ToggleVisibility(int id)
        {
            var value = _cvFileService.TGetByID(id);
            value.IsLatest = !value.IsLatest;
            _cvFileService.TUpdate(value);
            return RedirectToAction("Index", "CvFile", new { area = "Admin" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var value = _cvFileService.TGetByID(id);
            if (value != null)
            {
                // Fiziksel dosyayı sil
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", value.FileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                _cvFileService.TDelete(value);
            }
            return RedirectToAction("Index", "CvFile", new { area = "Admin" });
        }

        public IActionResult Download(int id)
        {
            var value = _cvFileService.TGetByID(id);
            if (value == null) return NotFound();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", value.FileName);
            if (!System.IO.File.Exists(path)) return NotFound();

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/pdf", value.Title + ".pdf");
        }
    }
}
