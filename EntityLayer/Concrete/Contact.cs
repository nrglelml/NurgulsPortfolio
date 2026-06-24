using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Contact
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Mail { get; set; }
        public string? Description { get; set; }

    }
}
