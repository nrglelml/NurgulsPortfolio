using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactMeManager:IContactMeService
    {
        IContactMeDal _contactMeDal;
        public ContactMeManager(IContactMeDal contactMeDal)
        {
            _contactMeDal = contactMeDal;
        }
        public void TAdd(ContactMe t)
        {
            _contactMeDal.Insert(t);
        }

        public void TDelete(ContactMe t)
        {
            _contactMeDal.Delete(t);
        }

        public ContactMe TGetByID(int id)
        {
            return _contactMeDal.GetByID(id);
        }

        public List<ContactMe> TGetList()
        {
            return _contactMeDal.GetList();
        }

        public List<ContactMe> TGetListByStatus(bool filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(ContactMe t)
        {
            _contactMeDal.Update(t);
        }
    }
}
