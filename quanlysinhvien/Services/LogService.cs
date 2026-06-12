// Services/LogManagementService.svc.cs
using quanlysinhvien.Helper;
using QuanLySinhVien.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace quanlysinhvien.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class LogService : ILogManagementService
    {
        private string _logPath => AppDomain.CurrentDomain.BaseDirectory + "logs/WCF.log";

        // Implementation — thay toàn bộ GetLogs
        public LogPagedResultDTO GetLogs(string page = "1", string pageSize = "20",
                                          string filter = "", string startDate = "", string endDate = "")
        {
            try
            {
                if (!File.Exists(_logPath))
                    return new LogPagedResultDTO { data = new List<LogEntryDTO>(), total = 0 };

                var lines = ReadAllLines(_logPath);

                if (!string.IsNullOrWhiteSpace(filter))
                    lines = lines.Where(l => l.Contains(filter)).ToList();

                if (!string.IsNullOrWhiteSpace(startDate) && !string.IsNullOrWhiteSpace(endDate))
                {
                    var start = DateTime.Parse(startDate);
                    var end = DateTime.Parse(endDate);
                    lines = lines.Where(l => {
                        try { var t = ExtractDateTime(l); return t >= start && t <= end; }
                        catch { return false; }
                    }).ToList();
                }

                var total = lines.Count;

                int pagez = 1, pageSizez = 20;
                try { pagez = int.Parse(page); pageSizez = int.Parse(pageSize); } catch { }

                var pageLines = lines
                    .Skip((pagez - 1) * pageSizez)
                    .Take(pageSizez)
                    .Reverse()
                    .ToList();

                return new LogPagedResultDTO
                {
                    data = pageLines.Select(l => ParseLogLine(l)).ToList(),
                    total = total
                };
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in GetLogs", ex);
                return new LogPagedResultDTO { data = new List<LogEntryDTO>(), total = 0 };
            }
        }

        public LogPaginationDTO GetPagination(string pageSize = "20", string filter = "", string startDate = "", string endDate = "")
        {
            try
            {
                if (!File.Exists(_logPath))
                    return new LogPaginationDTO { TotalPages = 0, TotalRecords = 0, CurrentPage = 1 };

                var lines = ReadAllLines(_logPath);

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    lines = lines.Where(l => l.Contains(filter)).ToList();
                }

                if (!string.IsNullOrWhiteSpace(startDate) && !string.IsNullOrWhiteSpace(endDate))
                {
                    var start = DateTime.Parse(startDate);
                    var end = DateTime.Parse(endDate);
                    lines = lines.Where(l =>
                    {
                        try
                        {
                            var logTime = ExtractDateTime(l);
                            return logTime >= start && logTime <= end;
                        }
                        catch { return false; }
                    }).ToList();
                }

                // Parse pageSize từ string
                int pageSizeInt = 20;
                try
                {
                    pageSizeInt = int.Parse(pageSize);
                }
                catch { }

                var totalRecords = lines.Count;

                // Sửa lỗi ambiguous: cast thành double
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeInt);

                return new LogPaginationDTO
                {
                    TotalPages = totalPages,
                    TotalRecords = totalRecords,
                    CurrentPage = 1,
                    PageSize = pageSizeInt
                };
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in GetPagination", ex);
                return new LogPaginationDTO { TotalPages = 0, TotalRecords = 0, CurrentPage = 1 };
            }
        }

        public bool ClearLogs()
        {
            try
            {
                if (File.Exists(_logPath))
                {
                    File.Delete(_logPath);
                    LogHelper.Info("Log file cleared successfully");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error clearing log file", ex);
                return false;
            }
        }

        public bool ExportLogs(string format = "txt")
        {
            try
            {
                if (!File.Exists(_logPath))
                    return false;

                var exportPath = AppDomain.CurrentDomain.BaseDirectory + "logs/WCF_export_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "." + format;

                var lines = ReadAllLines(_logPath);

                if (format == "txt")
                {
                    File.WriteAllLines(exportPath, lines);
                }
                else if (format == "csv")
                {
                    var csvLines = new List<string> { "Time,Level,Type,Message" };
                    foreach (var line in lines)
                    {
                        var entry = ParseLogLine(line);
                        csvLines.Add($"{entry.Time},{entry.Level},{entry.Type},{entry.Message}");
                    }
                    File.WriteAllLines(exportPath, csvLines);
                }

                LogHelper.Info($"Logs exported to: {exportPath}");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error exporting logs", ex);
                return false;
            }
        }

        public LogStatisticsDTO GetStatistics()
        {
            try
            {
                if (!File.Exists(_logPath))
                    return new LogStatisticsDTO();

                var lines = ReadAllLines(_logPath);

                var infoCount = lines.Count(l => l.Contains("INFO"));
                var errorCount = lines.Count(l => l.Contains("ERROR"));
                var warningCount = lines.Count(l => l.Contains("WARNING"));
                var createCount = lines.Count(l => l.Contains("Create"));
                var updateCount = lines.Count(l => l.Contains("Update"));
                var deleteCount = lines.Count(l => l.Contains("Delete"));

                return new LogStatisticsDTO
                {
                    TotalLines = lines.Count,
                    InfoCount = infoCount,
                    ErrorCount = errorCount,
                    WarningCount = warningCount,
                    CreateCount = createCount,
                    UpdateCount = updateCount,
                    DeleteCount = deleteCount
                };
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error getting statistics", ex);
                return new LogStatisticsDTO();
            }
        }

        private List<string> ReadAllLines(string path)
        {
            var lines = new List<string>();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private DateTime ExtractDateTime(string line)
        {
            try
            {
                var parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    var datePart = parts[0];
                    var timePart = parts[1].Split('.')[0];
                    return DateTime.Parse($"{datePart} {timePart}");
                }
            }
            catch { }
            return DateTime.MinValue;
        }

        private LogEntryDTO ParseLogLine(string line)
        {
            try
            {
                var parts = line.Split(new string[] { " - " }, StringSplitOptions.None);
                var dateTimeParts = parts[0].Split(' ');

                return new LogEntryDTO
                {
                    Time = dateTimeParts.Length >= 2 ? dateTimeParts[1] : "Unknown",
                    DateTime = ExtractDateTime(line).ToString("yyyy-MM-dd HH:mm:ss"),
                    Level = line.Contains("ERROR") ? "ERROR" : (line.Contains("WARNING") ? "WARNING" : "INFO"),
                    Type = ExtractType(line),
                    Message = parts.Length > 1 ? parts[1] : line
                };
            }
            catch
            {
                return new LogEntryDTO { Time = "Unknown", Message = line };
            }
        }

        private string ExtractType(string line)
        {
            if (line.Contains("GiangVien")) return "GiangVien";
            if (line.Contains("SinhVien")) return "SinhVien";
            if (line.Contains("Khoa")) return "Khoa";
            if (line.Contains("Lop")) return "Lop";
            if (line.Contains("MonHoc")) return "MonHoc";
            if (line.Contains("BangDiem")) return "BangDiem";
            if (line.Contains("CreateInfor")) return "Info";
            if (line.Contains("GetAll")) return "Read";
            if (line.Contains("Update")) return "Update";
            if (line.Contains("Delete")) return "Delete";
            return "System";
        }
    }

    [DataContract]
    public class LogEntryDTO
    {
        [DataMember] public string Time { get; set; }
        [DataMember] public string DateTime { get; set; }
        [DataMember] public string Level { get; set; }
        [DataMember] public string Type { get; set; }
        [DataMember] public string Message { get; set; }
    }

    [DataContract]
    public class LogPaginationDTO
    {
        [DataMember] public int TotalPages { get; set; }
        [DataMember] public int TotalRecords { get; set; }
        [DataMember] public int CurrentPage { get; set; }
        [DataMember] public int PageSize { get; set; }
    }

    [DataContract]
    public class LogStatisticsDTO
    {
        [DataMember] public int TotalLines { get; set; }
        [DataMember] public int InfoCount { get; set; }
        [DataMember] public int ErrorCount { get; set; }
        [DataMember] public int WarningCount { get; set; }
        [DataMember] public int CreateCount { get; set; }
        [DataMember] public int UpdateCount { get; set; }
        [DataMember] public int DeleteCount { get; set; }
    }
    [DataContract]
    public class LogPagedResultDTO
    {
        [DataMember] public List<LogEntryDTO> data { get; set; }
        [DataMember] public int total { get; set; }
    }

    [ServiceContract]
    public interface ILogManagementService
    {
        // Interface
        [OperationContract]
        [WebGet(UriTemplate = "GetLogs?page={page}&pageSize={pageSize}&filter={filter}&startDate={startDate}&endDate={endDate}",
                ResponseFormat = WebMessageFormat.Json)]
        LogPagedResultDTO GetLogs(string page = "1", string pageSize = "20",
                                   string filter = "", string startDate = "", string endDate = "");

        [OperationContract]
        [WebGet(UriTemplate = "GetPagination?pageSize={pageSize}&filter={filter}&startDate={startDate}&endDate={endDate}", ResponseFormat = WebMessageFormat.Json)]
        LogPaginationDTO GetPagination(string pageSize = "20", string filter = "", string startDate = "", string endDate = "");

        [OperationContract]
        [WebInvoke(Method = "DELETE")]
        bool ClearLogs();

        [OperationContract]
        [WebInvoke(Method = "POST")]
        bool ExportLogs(string format = "txt");

        [OperationContract]
        [WebGet(UriTemplate = "GetStatistics", ResponseFormat = WebMessageFormat.Json)]
        LogStatisticsDTO GetStatistics();
    }
}