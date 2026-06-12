using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.IServices
{
    [ServiceContract]
    public interface IBangdiemService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAll", ResponseFormat = WebMessageFormat.Json)]
        List<BANGDIEMDTO> GetAll();
        [OperationContract]
        [WebGet(UriTemplate = "GetById/{id}", ResponseFormat = WebMessageFormat.Json)]
        BANGDIEMDTO GetById(string id);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Create", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Create(BANGDIEMDTO bangdiem);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Update/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Update(string id, BANGDIEMDTO bangdiem);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Delete/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Delete(string id);
    }
}