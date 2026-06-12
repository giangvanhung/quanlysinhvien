using System.Configuration;

namespace QuanLySinhVien.Helpers
{
    public static class ConnectionStringHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["sinhvienDB"]
                .ConnectionString;
        }
    }
}