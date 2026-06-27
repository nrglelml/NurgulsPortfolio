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
    public class InterestManager : IinterestService
    {
        IinterestDal _interestDal;

        public InterestManager(IinterestDal interestDal)
        {
            _interestDal = interestDal;
        }

        public void TAdd(Interest t)
        {
            _interestDal.Insert(t);
        }

        public void TDelete(Interest t)
        {
            _interestDal.Delete(t);
        }

        public Interest TGetByID(int id)
        {
            return _interestDal.GetByID(id);
        }

        public List<Interest> TGetList()
        {
           return _interestDal.GetList();
        }

        public List<Interest> TGetListByStatus(bool filter)
        {
           return  _interestDal.GetListByFilter(x=>x.IsActive==filter);
        }

        public void TUpdate(Interest t)
        {
            _interestDal.Update(t);
        }
    }
}
