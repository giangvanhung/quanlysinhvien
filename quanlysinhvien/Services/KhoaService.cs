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
    public class KhoaService : IKhoaService
    {
        private readonly KhoaBLL _khoaBLL; 

        public KhoaService()
        {
            _khoaBLL = new KhoaBLL(); 
        }

        public List<KHOADTO> GetAll()
        {
            var entities = _khoaBLL.GetAll();
            return entities.Select(e => new KHOADTO
            {
                MaKhoa = e.MaKhoa,
                TenKhoa = e.TenKhoa
            }).ToList();
        }

        public KHOADTO GetById(string id)
        {
            var entity = _khoaBLL.GetById(id);
            if (entity == null) return null;

            return new KHOADTO
            {
                MaKhoa = entity.MaKhoa,
                TenKhoa = entity.TenKhoa
            };
        }

        public void Create(KHOADTO dto)
        {
            var entity = new KHOA
            {
                MaKhoa = dto.MaKhoa,
                TenKhoa = dto.TenKhoa
            };

            //Debug.WriteLine(entity.TenKhoa);

            _khoaBLL.Create(entity);
        }

        public void Update(string id, KHOADTO dto)
        {
            var entity = new KHOA
            {
                MaKhoa = id,  
                TenKhoa = dto.TenKhoa
            };

            _khoaBLL.Update(entity);
        }

        public void Delete(string id)
        {
            _khoaBLL.Delete(id);
        }
    }
}