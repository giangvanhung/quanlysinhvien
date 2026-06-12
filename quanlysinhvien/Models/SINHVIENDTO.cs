using System;
using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class SINHVIENDTO
    {
        [DataMember]
        public string MaSV { get; set; }
        [DataMember]
        public string TenSV { get; set; }
        [DataMember]
        public string NgaySinh { get; set; }
        [DataMember]
        public bool GioiTinh { get; set; }
        [DataMember]
        public string MaLop { get; set; }
    }
}