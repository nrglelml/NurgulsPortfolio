using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProjectImageManager : IProjectImageService
    {
        IProjectImageDal _projectImageDal;

        public ProjectImageManager(IProjectImageDal projectImageDal)
        {
            _projectImageDal = projectImageDal;
        }

        public void TAdd(ProjectImage t)
        {
            _projectImageDal.Insert(t);
        }

        public void TDelete(ProjectImage t)
        {
            _projectImageDal.Delete(t);
        }

        public ProjectImage TGetByID(int id)
        {
            return _projectImageDal.GetByID(id);
        }

        public List<ProjectImage> TGetList()
        {
            return _projectImageDal.GetList();
        }

        public List<ProjectImage> TGetListByStatus(bool filter)
        {
            return _projectImageDal.GetListByFilter(x => x.IsActive == filter);
        }

        public void TUpdate(ProjectImage t)
        {
            _projectImageDal.Update(t);
        }
    }
}
