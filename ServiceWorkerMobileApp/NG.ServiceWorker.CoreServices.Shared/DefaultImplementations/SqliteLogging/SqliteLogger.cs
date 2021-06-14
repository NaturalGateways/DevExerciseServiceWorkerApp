using System;
namespace NG.ServiceWorker.CoreServices.SqliteLogging
{
    public class SqliteLogger : ILogger
    {
        #region Base

        /// <summary>Main log service.</summary>
        private SqliteLogService m_sqliteLogService = null;

        /// <summary>Logger name.</summary>
        private string m_loggerName = null;

        /// <summary>Constructor.</summary>
        public SqliteLogger(SqliteLogService sqliteLogService, string loggerName)
        {
            m_sqliteLogService = sqliteLogService;
            m_loggerName = loggerName;
        }

        #endregion

        #region ILogger implementation

        public void Trace(string message)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Trace, message, null);
        }

        public void Info(string message)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Info, message, null);
        }

        public void Warn(string message)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Warn, message, null);
        }

        public void Error(string message)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Error, message, null);
        }

        public void Fatal(string message)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Fatal, message, null);
        }

        public void Warn(string message, Exception ex)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Warn, message, ex);
        }

        public void Error(string message, Exception ex)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Error, message, ex);
        }

        public void Fatal(string message, Exception ex)
        {
            m_sqliteLogService.InsertLogEntry(m_loggerName, LogLevel.Fatal, message, ex);
        }

        #endregion
    }
}
