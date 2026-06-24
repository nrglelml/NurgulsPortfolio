using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ContactMe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Mail { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public bool IsActive { get; set; }
    }
}
