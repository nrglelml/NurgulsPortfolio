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
    public class CvFileManager:ICvFileService
    {
        ICvFileDal _cvFileDal;
        public CvFileManager(ICvFileDal cvFileDal)
        {
            _cvFileDal = cvFileDal;
        }
        public void TAdd(CvFile t)
        {
            _cvFileDal.Insert(t);
        }

        public void TDelete(CvFile t)
        {
            _cvFileDal.Delete(t);
        }

        public CvFile TGetByID(int id)
        {
            return _cvFileDal.GetByID(id);
        }

        public List<CvFile> TGetList()
        {
            return _cvFileDal.GetList();
        }

        public List<CvFile> TGetListByStatus(bool filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(CvFile t)
        {
            _cvFileDal.Update(t);
        }
    }
}
