using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class AboutMeAddEditDTO
    {
        public int Id { get; set; }
        public string? ImageURL { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Brief { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Birthday { get; set; }
        public string? WorkStatus { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
