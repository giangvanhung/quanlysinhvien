using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.IServices
{
    [ServiceContract]
    public interface IKhoaService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAll", ResponseFormat = WebMessageFormat.Json)]
        List<KHOADTO> GetAll();
        [OperationContract]
        [WebGet(UriTemplate = "GetById/{id}", ResponseFormat = WebMessageFormat.Json)]
        KHOADTO GetById(string id);    
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Create", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Create(KHOADTO khoa);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Update/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Update(string id, KHOADTO khoa);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Delete/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Delete(string id);
    }
}
