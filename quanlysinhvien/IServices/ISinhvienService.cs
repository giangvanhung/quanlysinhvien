using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.IServices
{
    [ServiceContract]
    public interface ISinhvienService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAll", ResponseFormat = WebMessageFormat.Json)]
        List<SINHVIENDTO> GetAll();
        [OperationContract]
        [WebGet(UriTemplate = "GetById/{id}", ResponseFormat = WebMessageFormat.Json)]
        SINHVIENDTO GetById(string id);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Create", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Create(SINHVIENDTO sinhvien);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Update/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Update(string id, SINHVIENDTO sinhvien);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Delete/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Delete(string id);
        [OperationContract]
        [WebGet(UriTemplate = "/sinhvien/{maSV}/bangdiem", ResponseFormat = WebMessageFormat.Json)]
        List<BANGDIEMDTO> GetBangDiemBySinhVien(string maSV);

        [OperationContract]
        [WebGet(UriTemplate = "/sinhvien/{maSV}/infor", ResponseFormat = WebMessageFormat.Json)]
        List<INFORSINHVIENDTO> GetInforBySinhVien(string maSV);
    }
}