using quanlysinhvien.BLL;
using quanlysinhvien.IServices;
using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace quanlysinhvien.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BangdiemService : IBangdiemService
    {
        private readonly BangdiemBLL _bll;

        public BangdiemService()
        {
            _bll = new BangdiemBLL();
        }

        public List<BANGDIEMDTO> GetAll()
        {
            return _bll.GetAll();
        }

        // id = "MaSV|MaMon" — VD: "SV001|MON001"
        public BANGDIEMDTO GetById(string id)
        {
            var parts = id.Split('|');
            return _bll.GetById(parts[0], parts[1]);
        }

        public void Create(BANGDIEMDTO bangdiem)
        {
            _bll.Create(bangdiem);
        }

        public void Update(string id, BANGDIEMDTO bangdiem)
        {
            // B? split — id gi? ch? là s? nguyên "1", "2"...
            _bll.Update(id, bangdiem);
        }

        public void Delete(string id)
        {
            // B? split — id gi? ch? là s? nguyên "1", "2"...
            _bll.Delete(id);
        }
    }
}