using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.IServices
{
    [ServiceContract]
    public interface IMonhocService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAll", ResponseFormat = WebMessageFormat.Json)]
        List<MONHOCDTO> GetAll();
        [OperationContract]
        [WebGet(UriTemplate = "GetById/{id}", ResponseFormat = WebMessageFormat.Json)]
        MONHOCDTO GetById(string id);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Create", ResponseFormat = WebMessageFormat.Json)]
        void Create(MONHOCDTO monhoc);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Update/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Update(string id, MONHOCDTO monhoc);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Delete/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Delete(string id);
    }
}