using log4net;
using System;

namespace QuanLySinhVien.Helpers
{
    public static class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogHelper));

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Error(string message, Exception ex = null)
        {
            log.Error(message, ex);
        }

        public static void Warning(string message)
        {
            log.Warn(message);
        }
    }
}