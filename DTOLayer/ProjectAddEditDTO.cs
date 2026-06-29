using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DTOLayer
{
    public class ProjectAddEditDTO
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public string? ProjectUrl { get; set; }
        public string? TechStack { get; set; }
        public bool IsActive { get; set; }
        public int SkillId { get; set; }                 
        public IFormFile? CoverImageFile { get; set; }
        public string? CoverImageURL { get; set; }

        // Dropdown için — controller'dan View'a taşır
        public List<SelectListItem>? SkillList { get; set; }
    }
}
