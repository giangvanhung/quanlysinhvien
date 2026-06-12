using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class BANGDIEMDTO
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string MaSV { get; set; }
        [DataMember]
        public string MaMon { get; set; }
        [DataMember]
        public string MaGV { get; set; }
        [DataMember]
        public float DiemSo { get; set; }
        [DataMember]
        public string NamHoc { get; set; }
    }
}