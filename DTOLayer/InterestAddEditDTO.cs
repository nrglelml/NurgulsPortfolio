using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class InterestAddEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
