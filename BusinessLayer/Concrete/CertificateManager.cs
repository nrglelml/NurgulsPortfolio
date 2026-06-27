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
    public class CertificateManager:ICertificateService
    {
        ICertificateDal _certificateDal;
        public CertificateManager(ICertificateDal certificateDal)
        {
            _certificateDal = certificateDal;
        }
        public void TAdd(Certificate t)
        {
            _certificateDal.Insert(t);
        }

        public void TDelete(Certificate t)
        {
            _certificateDal.Delete(t);
        }

        public Certificate TGetByID(int id)
        {
            return _certificateDal.GetByID(id);
        }

        public List<Certificate> TGetList()
        {
            return _certificateDal.GetList();
        }

        public List<Certificate> TGetListByStatus(bool filter)
        {
            return _certificateDal.GetListByFilter(x => x.IsActive == filter);
        }

        public void TUpdate(Certificate t)
        {
            _certificateDal.Update(t);
        }
    }
}
