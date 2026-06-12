using QuanLySinhVien.Helpers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace quanlysinhvien.Helper
{
    public static class DBConnection
    {
        private readonly static string connectionString = ConnectionStringHelper.GetConnectionString();

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand CreateCommand(
            string sql,
            SqlConnection conn,
            Dictionary<string, object> parameters = null)
        {
            var cmd = new SqlCommand(sql, conn);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(new SqlParameter(param.Key, param.Value));
                }
            }

            return cmd;
        }

        public static SqlCommand ExecuteQuery(
            string sql,
            Dictionary<string, object> parameters = null)
        {
            var conn = GetConnection();
            var cmd = CreateCommand(sql, conn, parameters);
            return cmd;
        }
    }
}