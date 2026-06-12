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
    public class SinhvienService : ISinhvienService, IInforSinhvienService
    {
        private readonly SinhvienBLL _sinhvienBLL;

        public SinhvienService()
        {
            _sinhvienBLL = new SinhvienBLL();
        }

        public List<SINHVIENDTO> GetAll()
        {
            var entities = _sinhvienBLL.GetAll();
            return entities.Select(e => new SINHVIENDTO
            {
                MaSV = e.MaSV,
                TenSV = e.TenSV,
                NgaySinh = e.NgaySinh.ToString(),
                GioiTinh = e.GioiTinh,
                MaLop = e.MaLop
            }).ToList();
        }

        public SINHVIENDTO GetById(string id)
        {
            var entity = _sinhvienBLL.GetById(id);
            if (entity == null) return null;

            return new SINHVIENDTO
            {
                MaSV = entity.MaSV,
                TenSV = entity.TenSV,
                NgaySinh = entity.NgaySinh.ToString(),
                GioiTinh = entity.GioiTinh,
                MaLop = entity.MaLop
            };
        }

        public void Create(SINHVIENDTO dto)
        {
            var entity = new SINHVIEN
            {
                MaSV = dto.MaSV,
                TenSV = dto.TenSV,
                NgaySinh = dto.NgaySinh != null && dto.NgaySinh.Trim() != ""
            ? DateTime.Parse(dto.NgaySinh)
            : DateTime.Parse("1900-01-01"),
                GioiTinh = dto.GioiTinh,
                MaLop = dto.MaLop
            };

            _sinhvienBLL.Create(entity);
        }

        public void Update(string id, SINHVIENDTO dto)
        {
            var entity = new SINHVIEN
            {
                MaSV = id,
                TenSV = dto.TenSV,
                NgaySinh = dto.NgaySinh != null && dto.NgaySinh.Trim() != ""
            ? DateTime.Parse(dto.NgaySinh)
            : DateTime.Parse("1900-01-01"),
                GioiTinh = dto.GioiTinh,
                MaLop = dto.MaLop
            };

            _sinhvienBLL.Update(entity);
        }

        public void Delete(string id)
        {
            _sinhvienBLL.Delete(id);
        }

        public List<BANGDIEMDTO> GetBangDiemBySinhVien(string maSV)
        {
            var entities = _sinhvienBLL.GetBangDiem(maSV);
            return entities.Select(e => new BANGDIEMDTO
            {
                MaSV = e.MaSV,
                MaGV = e.MaGV,
                MaMon = e.MaMon,
                DiemSo = e.DiemSo,
                NamHoc = e.NamHoc
            }).ToList();
        }
        public List<INFORSINHVIENDTO> GetInforBySinhVien(string maSV)
        {
            var infor = _sinhvienBLL.GetInfor(maSV);
            return infor.Select(e => new INFORSINHVIENDTO
            {
                MaSV = e.MaSV,
                DanToc = e.DanToc,
                DiaChi = e.DiaChi,
                SDT = e.SDT,
                Email = e.Email,
                TonGiao = e.TonGiao,
            }).ToList();
        }

        public void CreateInfor(INFORSINHVIENDTO inforSinhviendto)
        {
            var infor = new INFORSINHVIEN()
            {
                DanToc = inforSinhviendto.DanToc,
                DiaChi = inforSinhviendto.DiaChi,
                SDT = inforSinhviendto.SDT,
                Email = inforSinhviendto.Email,
                MaSV = inforSinhviendto.MaSV,
                TonGiao = inforSinhviendto.TonGiao,
            };
            _sinhvienBLL.CreateInfor(infor);
        }
        public void UpdateInfor(string maSV, INFORSINHVIENDTO inforSinhviendto)
        {
            var infor = new INFORSINHVIEN()
            {
                DanToc = inforSinhviendto.DanToc,
                DiaChi = inforSinhviendto.DiaChi,
                SDT = inforSinhviendto.SDT,
                Email = inforSinhviendto.Email,
                TonGiao = inforSinhviendto.TonGiao,
            };
            _sinhvienBLL.UpdateInfor(maSV, infor);
        }
    }
}