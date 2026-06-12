using quanlysinhvien.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.IServices
{
    [ServiceContract]
    public interface IInforSinhvienService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreateInfor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CreateInfor(INFORSINHVIENDTO inforSinhvien);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "UpdateInfor/{maSV}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateInfor(string maSV, INFORSINHVIENDTO inforSinhvien);
    }
}