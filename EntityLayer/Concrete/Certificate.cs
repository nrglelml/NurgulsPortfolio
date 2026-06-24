using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Certificate
    {
        public int Id {  get; set; }
        public string Issuer { get; set; }
        public string Category { get; set; }
        public string? IconClass { get; set; }
        public string? IconBgColor { get; set; }
        public string CredentialURL { get; set; }
        public string CredentialId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
