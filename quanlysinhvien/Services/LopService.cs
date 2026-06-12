using quanlysinhvien.Business;
using quanlysinhvien.IServices;
using quanlysinhvien.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace quanlysinhvien.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LopService : ILopService
    {
        private readonly LopBLL _lopBLL;  

        public LopService()
        {
            _lopBLL = new LopBLL(); 
        }

        public List<LOPDTO> GetAll()  
        {
            var entities = _lopBLL.GetAll(); 
            return entities.Select(e => new LOPDTO  
            {
                MaLop = e.MaLop, 
                TenLop = e.TenLop, 
                MaKhoa = e.MaKhoa  
            }).ToList();
        }

        public LOPDTO GetById(string id)  
        {
            var entity = _lopBLL.GetById(id); 
            if (entity == null) return null;

            return new LOPDTO  
            {
                MaLop = entity.MaLop,  
                TenLop = entity.TenLop,  
                MaKhoa = entity.MaKhoa  
            };
        }

        public void Create(LOPDTO dto)  
        {
            var entity = new LOP  
            {
                MaLop = dto.MaLop, 
                TenLop = dto.TenLop,  
                MaKhoa = dto.MaKhoa  
            };

            Debug.WriteLine(entity.TenLop);  

            _lopBLL.Create(entity);  
        }

        public void Update(string id, LOPDTO dto)  
        {
            var entity = new LOP 
            {
                MaLop = id,  
                TenLop = dto.TenLop,  
                MaKhoa = dto.MaKhoa  
            };

            _lopBLL.Update(entity);  
        }

        public void Delete(string id)
        {
            _lopBLL.Delete(id);  
        }
    }
}