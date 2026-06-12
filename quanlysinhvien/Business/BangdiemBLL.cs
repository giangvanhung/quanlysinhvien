using System.Collections.Generic;
using quanlysinhvien.Data;
using quanlysinhvien.Models;

namespace quanlysinhvien.BLL
{
    public class BangdiemBLL
    {
        private readonly BangdiemRepository _repo;

        public BangdiemBLL()
        {
            _repo = new BangdiemRepository();
        }

        public List<BANGDIEMDTO> GetAll()
        {
            return _repo.GetAll();
        }

        public BANGDIEMDTO GetById(string maSV, string maMon)
        {
            return _repo.GetById(maSV, maMon);
        }

        public bool Create(BANGDIEMDTO bd)
        {
            return _repo.Create(bd);
        }

        public bool Update(string id, BANGDIEMDTO bd)
        {
            return _repo.Update(id, bd);
        }

        public bool Delete(string id)
        {
            return _repo.Delete(id);
        }
    }
}