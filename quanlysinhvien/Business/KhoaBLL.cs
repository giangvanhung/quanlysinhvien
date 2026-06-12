using System;
using System.Collections.Generic;
using System.Diagnostics;
using quanlysinhvien.Data;
using quanlysinhvien.Models;

namespace quanlysinhvien.Business
{
    public class KhoaBLL
    {
        private readonly KhoaRepository _repo = new KhoaRepository();

        public List<KHOA> GetAll()
        {
            return _repo.GetAll();
        }

        public KHOA GetById(string maKhoa)
        {
            return _repo.GetById(maKhoa);
        }

        public void Create(KHOA khoa)
        {
            if (string.IsNullOrEmpty(khoa.MaKhoa))
                throw new ArgumentException("MaKhoa is required");

            if (string.IsNullOrEmpty(khoa.TenKhoa))
                throw new ArgumentException("TenKhoa is required");

            //Debug.WriteLine(khoa.MaKhoa);
            _repo.Create(khoa);
        }

        public void Update(KHOA khoa)
        {
            if (string.IsNullOrEmpty(khoa.MaKhoa))
                throw new ArgumentException("MaKhoa is required");

            _repo.Update(khoa);
        }

        public void Delete(string maKhoa)
        {
            if (string.IsNullOrEmpty(maKhoa))
                throw new ArgumentException("MaKhoa is required");

            _repo.Delete(maKhoa);
        }
    }
}