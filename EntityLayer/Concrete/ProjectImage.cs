using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public string? ImageURL { get; set; }
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
