using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class KHOADTO
    {
        [DataMember]
        public string MaKhoa { get; set; }
        [DataMember]
        public string TenKhoa { get; set; }
    }
}