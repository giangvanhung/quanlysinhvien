using System;
using System.Collections.Generic;
using System.Diagnostics;
using quanlysinhvien.Data;
using quanlysinhvien.Models;

namespace quanlysinhvien.Business
{
    public class LopBLL
    {
        private readonly LopRepository _repo = new LopRepository();

        public List<LOP> GetAll()
        {
            return _repo.GetAll();
        }

        public LOP GetById(string maLop)
        {
            return _repo.GetById(maLop);
        }

        public void Create(LOP lop)
        {
            if (string.IsNullOrEmpty(lop.MaKhoa))
                throw new ArgumentException("MaKhoa is required");

            if (string.IsNullOrEmpty(lop.TenLop))
                throw new ArgumentException("TenLop is required");

            if (string.IsNullOrEmpty(lop.MaLop))
                throw new ArgumentException("MaLop is required");

            _repo.Create(lop);
        }

        public void Update(LOP lop)
        {
            if (string.IsNullOrEmpty(lop.MaLop))
            {
                throw new ArgumentException("maLop is required");
            }

            _repo.Update(lop);
        }

        public void Delete(string maLop)
        {
            if (string.IsNullOrEmpty(maLop))
                throw new ArgumentException("maLop is required");

            _repo.Delete(maLop);
        }
    }
}