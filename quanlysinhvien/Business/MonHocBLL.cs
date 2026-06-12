using System;
using System.Collections.Generic;
using System.Diagnostics;
using quanlysinhvien.Data;
using quanlysinhvien.Models;

namespace quanlysinhvien.Business
{
    public class MonHocBLL
    {
        private readonly MonHocRepository _repo = new MonHocRepository();

        public List<MONHOC> GetAll()
        {
            return _repo.GetAll();
        }

        public MONHOC GetById(string maMon)
        {
            return _repo.GetById(maMon);
        }

        public void Create(MONHOC mh)
        {
            if (string.IsNullOrEmpty(mh.MaMon))
                throw new ArgumentException("maMon is required");

            if (string.IsNullOrEmpty(mh.TenMon))
                throw new ArgumentException("maMon is required");

            //Debug.WriteLine(khoa.MaKhoa);
            _repo.Create(mh);
        }

        public void Update(MONHOC mh)
        {
            if (string.IsNullOrEmpty(mh.MaMon))
                throw new ArgumentException("maMon is required");

            _repo.Update(mh);
        }

        public void Delete(string maMon)
        {
            if (string.IsNullOrEmpty(maMon))
                throw new ArgumentException("maMon is required");

            _repo.Delete(maMon);
        }
    }
}