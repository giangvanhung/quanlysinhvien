using quanlysinhvien.Helper;
using quanlysinhvien.Models;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace quanlysinhvien.Data
{
    public class LopRepository
    {
        public List<LOP> GetAll()
        {
            LogHelper.Info("LopRepository.GetAll() called");
            var list = new List<LOP>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("LopRepository.GetAll() connection opened");

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaLop, TenLop, MaKhoa FROM LOP", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LOP
                            {
                                MaLop = reader.GetString(0),
                                TenLop = reader.GetString(1),
                                MaKhoa = reader.GetString(2)
                            });
                        }
                    }
                }

                if (list.Count == 0)
                {
                    LogHelper.Info("LopRepository.GetAll() returned 0 items (no Lop records found).");
                }
                else
                {
                    LogHelper.Info($"LopRepository.GetAll() returned {list.Count} items.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in LopRepository.GetAll()", ex);
            }

            return list;
        }

        public LOP GetById(string maLop)
        {
            LogHelper.Info($"LopRepository.GetById() called with MaLop: {maLop}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("LopRepository.GetById() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaLop", maLop }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaLop, TenLop, MaKhoa FROM LOP WHERE MaLop = @MaLop",
                        conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new LOP
                            {
                                MaLop = reader.GetString(0),
                                TenLop = reader.GetString(1),
                                MaKhoa = reader.GetString(2)
                            };
                            LogHelper.Info($"LopRepository.GetById() returned: MaLop={result.MaLop}, TenLop={result.TenLop}");
                            return result;
                        }
                    }
                }
                LogHelper.Warning($"LopRepository.GetById() returned null (not found) for MaLop: {maLop}");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in LopRepository.GetById() with MaLop={maLop}", ex);
            }
            return null;
        }

        public bool Create(LOP lop)
        {
            LogHelper.Info($"LopRepository.Create() called: MaLop={lop.MaLop}, TenLop={lop.TenLop}, MaKhoa={lop.MaKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("LopRepository.Create() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaLop", lop.MaLop },
                        { "@TenLop", lop.TenLop },
                        { "@MaKhoa", lop.MaKhoa }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES (@MaLop, @TenLop, @MaKhoa)",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"LopRepository.Create() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"LopRepository.Create() succeeded: MaLop={lop.MaLop}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"LopRepository.Create() failed: MaLop={lop.MaLop} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in LopRepository.Create() for MaLop={lop.MaLop}", ex);
                return false;
            }
        }

        public bool Update(LOP lop)
        {
            LogHelper.Info($"LopRepository.Update() called: MaLop={lop.MaLop}, TenLop={lop.TenLop}, MaKhoa={lop.MaKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("LopRepository.Update() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@TenLop", lop.TenLop },
                        { "@MaKhoa", lop.MaKhoa },
                        { "@MaLop", lop.MaLop }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "UPDATE LOP SET TenLop = @TenLop, MaKhoa = @MaKhoa WHERE MaLop = @MaLop",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"LopRepository.Update() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"LopRepository.Update() succeeded: MaLop={lop.MaLop}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"LopRepository.Update() failed: MaLop={lop.MaLop} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in LopRepository.Update() for MaLop={lop.MaLop}", ex);
                return false;
            }
        }

        public bool Delete(string maLop)
        {
            LogHelper.Info($"LopRepository.Delete() called with MaLop: {maLop}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("LopRepository.Delete() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaLop", maLop }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "DELETE FROM LOP WHERE MaLop = @MaLop",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"LopRepository.Delete() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"LopRepository.Delete() succeeded: MaLop={maLop}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"LopRepository.Delete() failed: MaLop={maLop} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in LopRepository.Delete() with MaLop={maLop}", ex);
                return false;
            }
        }
    }
}