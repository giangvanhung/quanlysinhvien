using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class LOPDTO
    {
        [DataMember]
        public string MaLop { get; set; }
        [DataMember]
        public string TenLop { get; set; }
        [DataMember]
        public string MaKhoa { get; set; }
    }
}