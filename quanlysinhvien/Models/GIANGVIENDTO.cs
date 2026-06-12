using System;
using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class GIANGVIENDTO
    {
        [DataMember]
        public string MaGV { get; set; }
        [DataMember]
        public string TenGV { get; set; }
        [DataMember]
        public string NgaySinh { get; set; }
        [DataMember]
        public bool GioiTinh { get; set; }
        [DataMember]
        public string MaKhoa { get; set; }
    }
}