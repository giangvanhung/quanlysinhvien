using quanlysinhvien.Helper;
using quanlysinhvien.Models;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace quanlysinhvien.Data
{
    public class SinhVienRepository
    {
        public List<SINHVIEN> GetAll()
        {
            LogHelper.Info("SinhVienRepository.GetAll() called");
            var list = new List<SINHVIEN>();
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.GetAll() connection opened");

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaSV, TenSV, NgaySinh, GioiTinh, MaLop FROM SINHVIEN", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SINHVIEN
                            {
                                MaSV = reader.GetString(0),
                                TenSV = reader.GetString(1),
                                NgaySinh = reader.GetDateTime(2),
                                GioiTinh = reader.GetBoolean(3),
                                MaLop = reader.GetString(4)
                            });
                        }
                    }
                }
                LogHelper.Info($"SinhVienRepository.GetAll() returned {list.Count} items");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in SinhVienRepository.GetAll()", ex);
            }
            return list;
        }

        public SINHVIEN GetById(string maSV)
        {
            LogHelper.Info($"SinhVienRepository.GetById() called with MaSV: {maSV}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.GetById() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV", maSV }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaSV, TenSV, NgaySinh, GioiTinh, MaLop FROM SINHVIEN WHERE MaSV = @MaSV",
                        conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new SINHVIEN
                            {
                                MaSV = reader.GetString(0),
                                TenSV = reader.GetString(1),
                                NgaySinh = reader.GetDateTime(2),
                                GioiTinh = reader.GetBoolean(3),
                                MaLop = reader.GetString(4)
                            };
                            LogHelper.Info($"SinhVienRepository.GetById() returned: MaSV={result.MaSV}, TenSV={result.TenSV}");
                            return result;
                        }
                    }
                }
                LogHelper.Info($"SinhVienRepository.GetById() returned null (not found) for MaSV: {maSV}");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.GetById() with MaSV={maSV}", ex);
            }

            return null;
        }

        public bool Create(SINHVIEN sv)
        {
            LogHelper.Info($"SinhVienRepository.Create() called: MaSV={sv.MaSV}, TenSV={sv.TenSV}, NgaySinh={sv.NgaySinh}, GioiTinh={sv.GioiTinh}, MaLop={sv.MaLop}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.Create() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV", sv.MaSV },
                        { "@TenSV", sv.TenSV },
                        { "@NgaySinh", sv.NgaySinh },
                        { "@GioiTinh", sv.GioiTinh },
                        { "@MaLop", sv.MaLop }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "INSERT INTO SINHVIEN (MaSV, TenSV, NgaySinh, GioiTinh, MaLop) VALUES (@MaSV, @TenSV, @NgaySinh, @GioiTinh, @MaLop)",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"SinhVienRepository.Create() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.Create() for MaSV={sv.MaSV}", ex);
            }
            return false;
        }

        public bool Update(SINHVIEN sv)
        {
            LogHelper.Info($"SinhVienRepository.Update() called: MaSV={sv.MaSV}, TenSV={sv.TenSV}, NgaySinh={sv.NgaySinh}, GioiTinh={sv.GioiTinh}, MaLop={sv.MaLop}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.Update() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@TenSV", sv.TenSV },
                        { "@NgaySinh", sv.NgaySinh },
                        { "@GioiTinh", sv.GioiTinh },
                        { "@MaLop", sv.MaLop },
                        { "@MaSV", sv.MaSV }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "UPDATE SINHVIEN SET TenSV = @TenSV, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, MaLop = @MaLop WHERE MaSV = @MaSV",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"SinhVienRepository.Update() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.Update() for MaSV={sv.MaSV}", ex);
            }
            return false;
        }

        public bool Delete(string maSV)
        {
            LogHelper.Info($"SinhVienRepository.Delete() called with MaSV: {maSV}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.Delete() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV", maSV }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "DELETE FROM SINHVIEN WHERE MaSV = @MaSV",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"SinhVienRepository.Delete() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.Delete() with MaSV={maSV}", ex);
            }
            return false;
        }

        public List<BANGDIEM> GetBangDiem(string maSV)
        {
            LogHelper.Info($"SinhVienRepository.GetBangDiem() called with MaSV: {maSV}");
            var list = new List<BANGDIEM>();
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.GetBangDiem() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV", maSV }
                    };
                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaSV, MaGV, MaMon, DiemSo, NamHoc FROM DIEM WHERE MaSV = @MaSV", conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new BANGDIEM
                            {
                                MaSV = reader.GetString(0),
                                MaGV = reader.GetString(1),
                                MaMon = reader.GetString(2),
                                DiemSo = (float)reader.GetDouble(3),
                                NamHoc = reader.GetString(4)
                            });
                        }
                    }
                }
                LogHelper.Info($"SinhVienRepository.GetBangDiem() returned {list.Count} items");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.GetBangDiem() with MaSV={maSV}", ex);
            }
            return list;
        }

        public List<INFORSINHVIEN> GetInfor(string maSV)
        {
            LogHelper.Info($"SinhVienRepository.GetInfor() called with MaSV: {maSV}");
            List<INFORSINHVIEN> list = new List<INFORSINHVIEN>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.GetInfor() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV", maSV }
                    };
                    SqlCommand cmd = DBConnection.CreateCommand("SELECT MaSV, DiaChi, TonGiao, SDT, Email, DanToc FROM INFORSINHVIEN WHERE MaSV = @MaSV", conn, parameters);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var info = new INFORSINHVIEN
                            {
                                MaSV = reader["MaSV"] as string,
                                DiaChi = reader["DiaChi"] as string,
                                TonGiao = reader["TonGiao"] as string,
                                SDT = reader["SDT"] as string,
                                Email = reader["Email"] as string,
                                DanToc = reader["DanToc"] as string
                            };
                            list.Add(info);
                        }
                    }
                }
                LogHelper.Info($"SinhVienRepository.GetInfor() returned {list.Count} items");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.GetInfor() with MaSV={maSV}", ex);
            }
            return list;
        }

        public bool CreateInfor(INFORSINHVIEN infor)
        {
            LogHelper.Info("CreateInfor started, Email: " + infor.Email);

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("Connection opened");

                    var parameters = new Dictionary<string, object> {
                        {"@DiaChi", infor.DiaChi == null ? (object)DBNull.Value : infor.DiaChi},
                        {"@TonGiao", infor.TonGiao == null ? (object)DBNull.Value : infor.TonGiao},
                        {"@SDT", infor.SDT == null ? (object)DBNull.Value : infor.SDT},
                        {"@Email", infor.Email == null ? (object)DBNull.Value : infor.Email},
                        {"@MaSV", infor.MaSV == null ? (object)DBNull.Value : infor.MaSV},
                        {"@DanToc", infor.DanToc == null ? (object)DBNull.Value : infor.DanToc}
                    };

                    string sql = @"INSERT INTO INFORSINHVIEN (DiaChi, TonGiao, SDT, Email, MaSV, DanToc) VALUES (@DiaChi, @TonGiao, @SDT, @Email, @MaSV, @DanToc)";
                    LogHelper.Info("SQL: " + sql);

                    using (var cmd = DBConnection.CreateCommand(sql, conn, parameters))
                    {
                        LogHelper.Info("Before ExecuteNonQuery");
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info("ExecuteNonQuery result: " + result);
                        LogHelper.Info("CreateInfor succeeded");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error CreateInfor", ex);
                LogHelper.Error("Exception Type: " + ex.GetType().Name);
                LogHelper.Error("Message: " + ex.Message);
            }
            return false;
        }

        public bool UpdateInfor(string maSV, INFORSINHVIEN infor)
        {
            LogHelper.Info($"SinhVienRepository.UpdateInfor() called with MaSV: {maSV}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("SinhVienRepository.UpdateInfor() connection opened");

                    var parameters = new Dictionary<string, object> {
                        {"@DiaChi", infor.DiaChi == null ? (object)DBNull.Value : infor.DiaChi},
                        {"@TonGiao", infor.TonGiao == null ? (object)DBNull.Value : infor.TonGiao},
                        {"@SDT", infor.SDT == null ? (object)DBNull.Value : infor.SDT},
                        {"@Email", infor.Email == null ? (object)DBNull.Value : infor.Email},
                        {"@DanToc", infor.DanToc == null ? (object)DBNull.Value : infor.DanToc},
                        {"@MaSV", maSV},
                    };

                    string sql = @"UPDATE INFORSINHVIEN SET DiaChi = @DiaChi, TonGiao = @TonGiao, SDT=@SDT, Email=@Email, DanToc=@DanToc WHERE MaSV=@MaSV";
                    LogHelper.Info("SQL: " + sql);

                    using (var cmd = DBConnection.CreateCommand(sql, conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"SinhVienRepository.UpdateInfor() ExecuteNonQuery result: {result}");

                        if (result > 0)
                        {
                            LogHelper.Info("SinhVienRepository.UpdateInfor() succeeded");
                            return true;
                        }
                        else
                        {
                            LogHelper.Error($"SinhVienRepository.UpdateInfor() failed (0 rows affected)");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in SinhVienRepository.UpdateInfor() with MaSV={maSV}", ex);
            }
            return false;
        }
    }
}