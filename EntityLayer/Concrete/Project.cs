using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string? CoverImageURL { get; set; }
        public string Description { get; set; }
        public string ProjectUrl{ get; set; }
        public string? TechStack { get; set; }
        public bool IsActive { get; set; }
        public int SkillId { get; set; }
        public Skill? Skill { get; set; }
        public List<ProjectImage> ProjectImages { get; set; }
    }
}
