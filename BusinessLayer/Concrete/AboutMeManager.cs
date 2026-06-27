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
    public class AboutMeManager : IAboutMeService
    {

        IAboutMeDal _aboutMeDal;
        public AboutMeManager(IAboutMeDal aboutMeDal)
        {
            _aboutMeDal = aboutMeDal;
        }
        public void TAdd(AboutMe t)
        {
            _aboutMeDal.Insert(t);
        }

        public void TDelete(AboutMe t)
        {
            _aboutMeDal.Delete(t);
        }

        public AboutMe TGetByID(int id)
        {
            return _aboutMeDal.GetByID(id);
        }

        public List<AboutMe> TGetList()
        {
            return _aboutMeDal.GetList();
        }

        public List<AboutMe> TGetListByStatus(bool filter)
        {
           throw new NotImplementedException();
        }

        public void TUpdate(AboutMe t)
        {
            _aboutMeDal.Update(t);
        }




    }
}
