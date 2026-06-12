using quanlysinhvien.Helper;
using quanlysinhvien.Models;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace quanlysinhvien.Data
{
    public class MonHocRepository
    {
        public List<MONHOC> GetAll()
        {
            LogHelper.Info("MonHocRepository.GetAll() called");
            var list = new List<MONHOC>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("MonHocRepository.GetAll() connection opened");

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaMon, TenMon FROM MONHOC", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new MONHOC
                            {
                                MaMon = reader.GetString(0),
                                TenMon = reader.GetString(1)
                            });
                        }
                    }
                }

                if (list.Count == 0)
                {
                    LogHelper.Info("MonHocRepository.GetAll() returned 0 items (no mon hoc records found).");
                }
                else
                {
                    LogHelper.Info($"MonHocRepository.GetAll() returned {list.Count} items.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in MonHocRepository.GetAll()", ex);
            }

            return list;
        }

        public MONHOC GetById(string maMon)
        {
            LogHelper.Info($"MonHocRepository.GetById() called with MaMon: {maMon}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("MonHocRepository.GetById() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaMon", maMon }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaMon, TenMon FROM MONHOC WHERE MaMon = @MaMon",
                        conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new MONHOC
                            {
                                MaMon = reader.GetString(0),
                                TenMon = reader.GetString(1)
                            };
                            LogHelper.Info($"MonHocRepository.GetById() returned: MaMon={result.MaMon}, TenMon={result.TenMon}");
                            return result;
                        }
                    }
                }
                LogHelper.Warning($"MonHocRepository.GetById() returned null (not found) for MaMon: {maMon}");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in MonHocRepository.GetById() with MaMon={maMon}", ex);
            }
            return null;
        }

        public bool Create(MONHOC mh)
        {
            LogHelper.Info($"MonHocRepository.Create() called: MaMon={mh.MaMon}, TenMon={mh.TenMon}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("MonHocRepository.Create() connection opened");

                    Debug.WriteLine(mh.TenMon);
                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaMon", mh.MaMon },
                        { "@TenMon", mh.TenMon }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "INSERT INTO MONHOC (MaMon, TenMon) VALUES (@MaMon, @TenMon)",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"MonHocRepository.Create() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"MonHocRepository.Create() succeeded: MaMon={mh.MaMon}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"MonHocRepository.Create() failed: MaMon={mh.MaMon} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in MonHocRepository.Create() for MaMon={mh.MaMon}", ex);
                return false;
            }
        }

        public bool Update(MONHOC mh)
        {
            LogHelper.Info($"MonHocRepository.Update() called: MaMon={mh.MaMon}, TenMon={mh.TenMon}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("MonHocRepository.Update() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@TenMon", mh.TenMon },
                        { "@MaMon", mh.MaMon }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "UPDATE MONHOC SET TenMon = @TenMon WHERE MaMon = @MaMon",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"MonHocRepository.Update() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"MonHocRepository.Update() succeeded: MaMon={mh.MaMon}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"MonHocRepository.Update() failed: MaMon={mh.MaMon} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in MonHocRepository.Update() for MaMon={mh.MaMon}", ex);
                return false;
            }
        }

        public bool Delete(string maMon)
        {
            LogHelper.Info($"MonHocRepository.Delete() called with MaMon: {maMon}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("MonHocRepository.Delete() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaMon", maMon }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "DELETE FROM MONHOC WHERE MaMon = @MaMon",
                        conn, parameters))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        LogHelper.Info($"MonHocRepository.Delete() ExecuteNonQuery result: {rowsAffected}");

                        if (rowsAffected > 0)
                        {
                            LogHelper.Info($"MonHocRepository.Delete() succeeded: MaMon={maMon}");
                            return true;
                        }
                    }
                }

                LogHelper.Error($"MonHocRepository.Delete() failed: MaMon={maMon} (0 rows affected)");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in MonHocRepository.Delete() with MaMon={maMon}", ex);
                return false;
            }
        }
    }
}