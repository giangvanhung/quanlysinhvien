using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.IServices
{
    [ServiceContract]
    public interface IInforGiangvienService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAll", ResponseFormat = WebMessageFormat.Json)]
        List<INFORGIANGVIENDTO> GetAll();
        [OperationContract]
        [WebGet(UriTemplate = "GetById?id={id}", ResponseFormat = WebMessageFormat.Json)]
        INFORGIANGVIENDTO GetById(string id);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Create", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Create(INFORGIANGVIENDTO inforGiangvien);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Update?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Update(string id, INFORGIANGVIENDTO inforGiangvien);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Delete?id={id}", ResponseFormat = WebMessageFormat.Json)]
        void Delete(string id);
    }
}