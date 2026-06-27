using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class ProjectImageAddEditDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageURL { get; set; }
    }
}
