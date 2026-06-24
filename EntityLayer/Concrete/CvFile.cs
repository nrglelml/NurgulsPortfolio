using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CvFile
    {
        public int Id { get; set; }
        public string Title { get; set; }       
        public string FileName { get; set; }    
        public long FileSizeBytes { get; set; }  
        public DateTime UploadedAt { get; set; }
        public bool IsLatest { get; set; }       
        public int DisplayOrder { get; set; }
    }
}
