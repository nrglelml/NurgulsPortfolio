using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class Experience
    {
        public int Id { get; set; }
      //  public string Title { get; set; }
        public string? Company { get; set; }
       public string? Description { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
