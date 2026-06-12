using quanlysinhvien.Business;
using quanlysinhvien.IServices;
using quanlysinhvien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;

namespace quanlysinhvien.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MonHocService : IMonhocService
    {
        private readonly MonHocBLL _monhocBLL = new MonHocBLL(); 
        public List<MONHOCDTO> GetAll()
        {
            var entities = _monhocBLL.GetAll();
            return entities.Select(e => new MONHOCDTO
            {
                MaMon = e.MaMon,
                TenMon = e.TenMon
            }).ToList();
        }
        public MONHOCDTO GetById(string id)
        {
            var monhoc = _monhocBLL.GetById(id);
            return new MONHOCDTO
            {
                MaMon = monhoc.MaMon,
                TenMon = monhoc.TenMon
            };
        }
        public void Create(MONHOCDTO monhoc)
        {
            var entity = new MONHOC
            {
                MaMon = monhoc.MaMon,
                TenMon = monhoc.TenMon
            };

            _monhocBLL.Create(entity);
        }
        public void Update(string id, MONHOCDTO monhoc)
        {
            var entity = new MONHOC
            {
                MaMon = id,
                TenMon = monhoc.TenMon
            };

            _monhocBLL.Update(entity);
        }
        public void Delete(string id)
        {
            _monhocBLL.Delete(id);
        }
    }

}