using System;
using System.Collections.Generic;
using log4net.Core;
using quanlysinhvien.Data;
using quanlysinhvien.Models;

namespace quanlysinhvien.Business
{
    public class SinhvienBLL
    {
        private readonly SinhVienRepository _repo = new SinhVienRepository();

        public List<SINHVIEN> GetAll()
        {
            return _repo.GetAll();
        }

        public SINHVIEN GetById(string maSV)
        {
            return _repo.GetById(maSV);
        }

        public bool Create(SINHVIEN sv)
        {
            if (string.IsNullOrEmpty(sv.MaSV))
                throw new ArgumentException("MaSV is required");

            if (string.IsNullOrEmpty(sv.TenSV))
                throw new ArgumentException("TenSV is required");

            return _repo.Create(sv);
        }

        public bool Update(SINHVIEN sv)
        {
            if (string.IsNullOrEmpty(sv.MaSV))
                throw new ArgumentException("MaSV is required");

            return _repo.Update(sv);
        }

        public bool Delete(string maSV)
        {
            if (string.IsNullOrEmpty(maSV))
                throw new ArgumentException("MaSV is required");

            return _repo.Delete(maSV);
        }

        public List<BANGDIEM> GetBangDiem(string maSV)
        {
            if (string.IsNullOrEmpty(maSV))
            {
                throw new ArgumentException("MaSV is required");
            }
            return _repo.GetBangDiem(maSV);
        }

        public List<INFORSINHVIEN> GetInfor(string maSV)
        {
            if (string.IsNullOrEmpty(maSV))
            {
                throw new ArgumentException("MaSV is required");
            }
            return _repo.GetInfor(maSV);
        }
        
        public bool CreateInfor(INFORSINHVIEN infor)
        {
            return _repo.CreateInfor(infor);
        }
        public bool UpdateInfor(string maSV, INFORSINHVIEN infor)
        {
            return _repo.UpdateInfor(maSV, infor);
        }

    }
}