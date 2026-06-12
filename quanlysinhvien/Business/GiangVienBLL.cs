using System;
using System.Collections.Generic;
using quanlysinhvien.Data;
using quanlysinhvien.Models;

namespace quanlysinhvien.Business
{
    public class GiangVienBLL
    {
        private readonly GiangVienRepository _repo = new GiangVienRepository();

        public List<GIANGVIEN> GetAll()
        {
            return _repo.GetAll();
        }

        public GIANGVIEN GetById(string maGV)
        {
            return _repo.GetById(maGV);
        }

        public bool Create(GIANGVIEN gv)
        {
            if (string.IsNullOrEmpty(gv.MaGV))
                throw new ArgumentException("MaSV is required");

            if (string.IsNullOrEmpty(gv.TenGV))
                throw new ArgumentException("TenSV is required");

            return _repo.Create(gv);
        }

        public bool Update(GIANGVIEN gv)
        {
            if (string.IsNullOrEmpty(gv.MaGV))
                throw new ArgumentException("MaSV is required");

            return _repo.Update(gv);
        }

        public bool Delete(string maGV)
        {
            if (string.IsNullOrEmpty(maGV))
                throw new ArgumentException("MaSV is required");

            return _repo.Delete(maGV);
        }
    }
}