using quanlysinhvien.Helper;
using quanlysinhvien.Models;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;

namespace quanlysinhvien.Data
{
    public class GiangVienRepository
    {
        public List<GIANGVIEN> GetAll()
        {
            LogHelper.Info("GiangVienRepository.GetAll() called");
            var list = new List<GIANGVIEN>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("GiangVienRepository.GetAll() connection opened");

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaGV, TenGV, NgaySinh, GioiTinh, MaKhoa FROM GIANGVIEN", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new GIANGVIEN
                            {
                                MaGV = reader.GetString(0),
                                TenGV = reader.GetString(1),
                                NgaySinh = reader.GetDateTime(2),
                                GioiTinh = reader.GetBoolean(3),
                                MaKhoa = reader.GetString(4)
                            });
                        }
                    }
                }
                LogHelper.Info($"GiangVienRepository.GetAll() returned {list.Count} items");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in GiangVienRepository.GetAll()", ex);
            }

            return list;
        }

        public GIANGVIEN GetById(string maGV)
        {
            LogHelper.Info($"GiangVienRepository.GetById() called with MaGV: {maGV}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("GiangVienRepository.GetById() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaGV", maGV }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaGV, TenGV, NgaySinh, GioiTinh, MaKhoa FROM GIANGVIEN WHERE MaGV = @MaGV",
                        conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new GIANGVIEN
                            {
                                MaGV = reader.GetString(0),
                                TenGV = reader.GetString(1),
                                NgaySinh = reader.GetDateTime(2),
                                GioiTinh = reader.GetBoolean(3),
                                MaKhoa = reader.GetString(4)
                            };
                            LogHelper.Info($"GiangVienRepository.GetById() returned: MaGV={result.MaGV}, TenGV={result.TenGV}");
                            return result;
                        }
                    }
                }
                LogHelper.Info($"GiangVienRepository.GetById() returned null (not found) for MaGV: {maGV}");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in GiangVienRepository.GetById() with MaGV={maGV}", ex);
            }

            return null;
        }

        public bool Create(GIANGVIEN gv)
        {
            LogHelper.Info($"GiangVienRepository.Create() called: MaGV={gv.MaGV}, TenGV={gv.TenGV}, NgaySinh={gv.NgaySinh}, GioiTinh={gv.GioiTinh}, MaKhoa={gv.MaKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("GiangVienRepository.Create() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaGV", gv.MaGV },
                        { "@TenGV", gv.TenGV },
                        { "@NgaySinh", gv.NgaySinh },
                        { "@GioiTinh", gv.GioiTinh },
                        { "@MaKhoa", gv.MaKhoa }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "INSERT INTO GIANGVIEN (MaGV, TenGV, NgaySinh, GioiTinh, MaKhoa) VALUES (@MaGV, @TenGV, @NgaySinh, @GioiTinh, @MaKhoa)",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"GiangVienRepository.Create() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in GiangVienRepository.Create() for MaGV={gv.MaGV}", ex);
            }
            return false;
        }

        public bool Update(GIANGVIEN gv)
        {
            LogHelper.Info($"GiangVienRepository.Update() called: MaGV={gv.MaGV}, TenGV={gv.TenGV}, NgaySinh={gv.NgaySinh}, GioiTinh={gv.GioiTinh}, MaKhoa={gv.MaKhoa}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("GiangVienRepository.Update() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@TenGV", gv.TenGV },
                        { "@NgaySinh", gv.NgaySinh },
                        { "@GioiTinh", gv.GioiTinh },
                        { "@MaKhoa", gv.MaKhoa },
                        { "@MaGV", gv.MaGV }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "UPDATE GIANGVIEN SET TenGV = @TenGV, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, MaKhoa = @MaKhoa WHERE MaGV = @MaGV",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"GiangVienRepository.Update() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in GiangVienRepository.Update() for MaGV={gv.MaGV}", ex);
            }
            return false;
        }

        public bool Delete(string maGV)
        {
            LogHelper.Info($"GiangVienRepository.Delete() called with MaGV: {maGV}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("GiangVienRepository.Delete() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaGV", maGV }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "DELETE FROM GIANGVIEN WHERE MaGV = @MaGV",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"GiangVienRepository.Delete() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in GiangVienRepository.Delete() with MaGV={maGV}", ex);
            }
            return false;
        }
    }
}