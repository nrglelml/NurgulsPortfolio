using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer;
using Microsoft.AspNetCore.Mvc;

namespace NurgulsPortfolio.Controllers
{
    public class CvFileController :BaseUIController
    {
        private readonly ICvFileService _cvFileService;
        private readonly IMapper _mapper;

        public CvFileController(ICvFileService cvFileService, IMapper mapper)
        {
            _cvFileService = cvFileService;
            _mapper = mapper;
        }
        [Route("/CvFile")]
        public IActionResult Index()
        {
            var values = _cvFileService.TGetList();
            var dtoList = _mapper.Map<List<CvFileAddEditDTO>>(values);
            return View(dtoList);
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
