using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProjectImageService:IGenericService<ProjectImage>
    {
        public List<ProjectImage> TGetListByProjectId(int projectId);
    }
}
