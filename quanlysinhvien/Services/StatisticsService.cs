// Services/StatisticsService.cs
using quanlysinhvien.Data;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class StatisticsService : IStatisticsService
    {
        public StatisticsDTO GetStatistics()
        {
            try
            {
                var sinhVienRepo = new SinhVienRepository();
                var lopRepo = new LopRepository();
                var giangVienRepo = new GiangVienRepository();
                var monHocRepo = new MonHocRepository();

                var totalSinhVien = sinhVienRepo.GetAll().Count;
                var totalLop = lopRepo.GetAll().Count;
                var totalGiangVien = giangVienRepo.GetAll().Count;
                var totalMonHoc = monHocRepo.GetAll().Count;

                return new StatisticsDTO
                {
                    TotalSinhVien = totalSinhVien,
                    TotalLop = totalLop,
                    TotalGiangVien = totalGiangVien,
                    TotalMonHoc = totalMonHoc,
                    LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error getting statistics", ex);
                return new StatisticsDTO
                {
                    TotalSinhVien = 0,
                    TotalLop = 0,
                    TotalGiangVien = 0,
                    TotalMonHoc = 0,
                    LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
        }
    }

    [DataContract]
    public class StatisticsDTO
    {
        [DataMember]
        public int TotalSinhVien { get; set; }

        [DataMember]
        public int TotalLop { get; set; }

        [DataMember]
        public int TotalGiangVien { get; set; }

        [DataMember]
        public int TotalMonHoc { get; set; }

        [DataMember]
        public string LastUpdated { get; set; }
    }

    [ServiceContract]
    public interface IStatisticsService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetStatistics", ResponseFormat = WebMessageFormat.Json)]
        StatisticsDTO GetStatistics();
    }
}