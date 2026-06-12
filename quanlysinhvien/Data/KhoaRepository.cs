using quanlysinhvien.Helper;
using quanlysinhvien.Models;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace quanlysinhvien.Data
{
    public class KhoaRepository
    {
        public List<KHOA> GetAll()
        {
            LogHelper.Info("KhoaRepository.GetAll() called");
            var list = new List<KHOA>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("KhoaRepository.GetAll() connection opened");

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaKhoa, TenKhoa FROM KHOA", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new KHOA
                            {
                                MaKhoa = reader.GetString(0),
                                TenKhoa = reader.GetString(1)
                            });
                        }
                    }
                }

                if (list.Count == 0)
                {
                    LogHelper.Info("KhoaRepository.GetAll() returned 0 items (no Khoa records found).");
                }
                else
                {
                    LogHelper.Info($"KhoaRepository.GetAll() returned {list.Count} items.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in KhoaRepository.GetAll()", ex);
            }

            return list;
        }

        public KHOA GetById(string maKhoa)
        {
            LogHelper.Info($"KhoaRepository.GetById() called with MaKhoa: {maKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("KhoaRepository.GetById() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaKhoa", maKhoa }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaKhoa, TenKhoa FROM KHOA WHERE MaKhoa = @MaKhoa",
                        conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new KHOA
                            {
                                MaKhoa = reader.GetString(0),
                                TenKhoa = reader.GetString(1)
                            };
                            LogHelper.Info($"KhoaRepository.GetById() returned: MaKhoa={result.MaKhoa}, TenKhoa={result.TenKhoa}");
                            return result;
                        }
                    }
                }
                LogHelper.Warning($"KhoaRepository.GetById() returned null (not found) for MaKhoa: {maKhoa}");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in KhoaRepository.GetById() with MaKhoa={maKhoa}", ex);
            }
            return null;
        }

        public bool Create(KHOA khoa)
        {
            LogHelper.Info($"KhoaRepository.Create() called: MaKhoa={khoa.MaKhoa}, TenKhoa={khoa.TenKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("KhoaRepository.Create() connection opened");

                    Debug.WriteLine(khoa.TenKhoa);
                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaKhoa", khoa.MaKhoa },
                        { "@TenKhoa", khoa.TenKhoa }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "INSERT INTO KHOA (MaKhoa, TenKhoa) VALUES (@MaKhoa, @TenKhoa)",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"KhoaRepository.Create() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"KhoaRepository.Create() succeeded: MaKhoa={khoa.MaKhoa}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"KhoaRepository.Create() failed: MaKhoa={khoa.MaKhoa} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in KhoaRepository.Create() for MaKhoa={khoa.MaKhoa}", ex);
                return false;
            }
        }

        public bool Update(KHOA khoa)
        {
            LogHelper.Info($"KhoaRepository.Update() called: MaKhoa={khoa.MaKhoa}, TenKhoa={khoa.TenKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("KhoaRepository.Update() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@TenKhoa", khoa.TenKhoa },
                        { "@MaKhoa", khoa.MaKhoa }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "UPDATE KHOA SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"KhoaRepository.Update() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"KhoaRepository.Update() succeeded: MaKhoa={khoa.MaKhoa}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"KhoaRepository.Update() failed: MaKhoa={khoa.MaKhoa} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in KhoaRepository.Update() for MaKhoa={khoa.MaKhoa}", ex);
                return false;
            }
        }

        public bool Delete(string maKhoa)
        {
            LogHelper.Info($"KhoaRepository.Delete() called with MaKhoa: {maKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("KhoaRepository.Delete() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaKhoa", maKhoa }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "DELETE FROM KHOA WHERE MaKhoa = @MaKhoa",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"KhoaRepository.Delete() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"KhoaRepository.Delete() succeeded: MaKhoa={maKhoa}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"KhoaRepository.Delete() failed: MaKhoa={maKhoa} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in KhoaRepository.Delete() with MaKhoa={maKhoa}", ex);
                return false;
            }
        }
    }
}