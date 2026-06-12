using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class INFORGIANGVIENDTO
    {
        [DataMember]
        public string MaGV { get; set; }
        [DataMember]
        public string DiaChi { get; set; }
        [DataMember]
        public string SDT { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string DanToc { get; set; }
        [DataMember]
        public string TonGiao { get; set; }
    }
}