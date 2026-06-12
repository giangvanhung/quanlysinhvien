using System.Runtime.Serialization;

namespace quanlysinhvien.Models
{
    [DataContract]
    public class MONHOCDTO
    {
        [DataMember]
        public string MaMon { get; set; }
        [DataMember]
        public string TenMon { get; set; }
    }
}