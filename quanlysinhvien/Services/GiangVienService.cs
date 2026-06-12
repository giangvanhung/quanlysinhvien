using quanlysinhvien.Business;
using quanlysinhvien.IServices;
using quanlysinhvien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace quanlysinhvien.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GiangVienService : IGiangvienService
    {
        private readonly GiangVienBLL _giangvienBLL;

        public GiangVienService()
        {
            _giangvienBLL = new GiangVienBLL();
        }

        public List<GIANGVIENDTO> GetAll()
        {
            var entities = _giangvienBLL.GetAll();
            return entities.Select(e => new GIANGVIENDTO
            {
                MaGV = e.MaGV,
                TenGV = e.TenGV,
                NgaySinh = e.NgaySinh.ToString(),
                GioiTinh = e.GioiTinh,
                MaKhoa = e.MaKhoa
            }).ToList();
        }

        public GIANGVIENDTO GetById(string id)
        {
            var entity = _giangvienBLL.GetById(id);
            if (entity == null) return null;

            return new GIANGVIENDTO
            {
                MaGV = entity.MaGV,
                TenGV = entity.TenGV,
                NgaySinh = entity.NgaySinh.ToString(),
                GioiTinh = entity.GioiTinh,
                MaKhoa = entity.MaKhoa
            };
        }

        public void Create(GIANGVIENDTO dto)
        {
            var entity = new GIANGVIEN
            {
                MaGV = dto.MaGV,
                TenGV = dto.TenGV,
                NgaySinh = dto.NgaySinh != null && dto.NgaySinh.Trim() != ""
            ? DateTime.Parse(dto.NgaySinh)
            : DateTime.Parse("1900-01-01"),
                GioiTinh = dto.GioiTinh,
                MaKhoa = dto.MaKhoa
            };

            _giangvienBLL.Create(entity);
        }

        public void Update(string id, GIANGVIENDTO dto)
        {
            var entity = new GIANGVIEN
            {
                MaGV = id,
                TenGV = dto.TenGV,
                NgaySinh = dto.NgaySinh != null && dto.NgaySinh.Trim() != ""
            ? DateTime.Parse(dto.NgaySinh)
            : DateTime.Parse("1900-01-01"),
                GioiTinh = dto.GioiTinh,
                MaKhoa = dto.MaKhoa
            };

            _giangvienBLL.Update(entity);
        }

        public void Delete(string id)
        {
            _giangvienBLL.Delete(id);
        }
    }
}