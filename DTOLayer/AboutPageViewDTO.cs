using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class AboutPageViewDTO
    {
        public AboutMeAddEditDTO AboutMe { get; set; }
        public Contact Contact { get; set; }
    }
}
