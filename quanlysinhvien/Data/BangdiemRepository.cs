using quanlysinhvien.Helper;
using quanlysinhvien.Models;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;

namespace quanlysinhvien.Data
{
    public class BangdiemRepository
    {
        public List<BANGDIEMDTO> GetAll()
        {
            LogHelper.Info("BangdiemRepository.GetAll() called");
            var list = new List<BANGDIEMDTO>();

            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("BangdiemRepository.GetAll() connection opened");

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT Id, MaSV, MaMon, MaGV, DiemSo, NamHoc FROM DIEM", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new BANGDIEMDTO
                            {
                                Id = reader.GetInt32(0),
                                MaSV = reader.GetString(1),
                                MaMon = reader.GetString(2),
                                MaGV = reader.GetString(3),
                                DiemSo = (float)reader.GetDouble(4),
                                NamHoc = reader.GetString(5)
                            });
                        }
                    }
                }
                LogHelper.Info($"BangdiemRepository.GetAll() returned {list.Count} items");

            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in BangdiemRepository.GetAll()", ex);
            }

            return list;
        }

        public BANGDIEMDTO GetById(string maSV, string maMon)
        {
            LogHelper.Info($"BangdiemRepository.GetById() called with MaSV: {maSV}, MaMon: {maMon}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("BangdiemRepository.GetById() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV",  maSV  },
                        { "@MaMon", maMon }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "SELECT MaSV, MaMon, MaGV, DiemSo, NamHoc FROM DIEM WHERE MaSV = @MaSV AND MaMon = @MaMon",
                        conn, parameters))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new BANGDIEMDTO
                            {
                                MaSV = reader.GetString(0),
                                MaMon = reader.GetString(1),
                                MaGV = reader.GetString(2),
                                DiemSo = (float)reader.GetDouble(3),
                                NamHoc = reader.GetString(4)
                            };
                            LogHelper.Info($"BangdiemRepository.GetById() returned: MaSV={result.MaSV}, MaMon={result.MaMon}, DiemSo={result.DiemSo}");
                            return result;
                        }
                    }
                }
                LogHelper.Info("BangdiemRepository.GetById() returned null (not found)");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in BangdiemRepository.GetById() with MaSV={maSV}, MaMon={maMon}", ex);
            }
            return null;
        }

        public bool Create(BANGDIEMDTO bd)
        {
            LogHelper.Info($"BangdiemRepository.Create() called: MaSV={bd.MaSV}, MaMon={bd.MaMon}, MaGV={bd.MaGV}, DiemSo={bd.DiemSo}, NamHoc={bd.NamHoc}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("BangdiemRepository.Create() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@MaSV",   bd.MaSV   },
                        { "@MaMon",  bd.MaMon  },
                        { "@MaGV",   bd.MaGV   },
                        { "@DiemSo", bd.DiemSo },
                        { "@NamHoc", bd.NamHoc }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "INSERT INTO DIEM (MaSV, MaMon, MaGV, DiemSo, NamHoc) VALUES (@MaSV, @MaMon, @MaGV, @DiemSo, @NamHoc)",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"BangdiemRepository.Create() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in BangdiemRepository.Create() for MaSV={bd.MaSV}, MaMon={bd.MaMon}", ex);
            }
            return false;
        }

        public bool Update(string id, BANGDIEMDTO bd)
        {
            LogHelper.Info($"BangdiemRepository.Update() called with Id: {id}, MaSV={bd.MaSV}, MaMon={bd.MaMon}, MaGV={bd.MaGV}, DiemSo={bd.DiemSo}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("BangdiemRepository.Update() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@Id",     int.Parse(id) },
                        { "@MaSV",   bd.MaSV       },
                        { "@MaMon",  bd.MaMon      },
                        { "@MaGV",   bd.MaGV       },
                        { "@DiemSo", bd.DiemSo     },
                        { "@NamHoc", bd.NamHoc     }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "UPDATE DIEM SET MaSV=@MaSV, MaMon=@MaMon, MaGV=@MaGV, DiemSo=@DiemSo, NamHoc=@NamHoc WHERE Id=@Id",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"BangdiemRepository.Update() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in BangdiemRepository.Update() with Id={id}", ex);
            }
            return false;
        }

        public bool Delete(string id)
        {
            LogHelper.Info($"BangdiemRepository.Delete() called with Id: {id}");
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    LogHelper.Info("BangdiemRepository.Delete() connection opened");

                    var parameters = new Dictionary<string, object>
                    {
                        { "@Id", int.Parse(id) }
                    };

                    using (var cmd = DBConnection.CreateCommand(
                        "DELETE FROM DIEM WHERE Id=@Id",
                        conn, parameters))
                    {
                        int result = cmd.ExecuteNonQuery();
                        LogHelper.Info($"BangdiemRepository.Delete() ExecuteNonQuery result: {result}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error in BangdiemRepository.Delete() with Id={id}", ex);
            }
            return false;
        }
    }
}