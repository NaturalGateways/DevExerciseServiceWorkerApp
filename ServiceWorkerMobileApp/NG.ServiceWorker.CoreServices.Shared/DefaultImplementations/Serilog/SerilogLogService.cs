using System;
using System.Collections.Generic;

using Serilog;

namespace NG.ServiceWorker.CoreServices.Serilog
{
    public class SerilogLogService : ILogService
    {
        #region Base

        /// <summary>The cached created loggers.</summary>
        private Dictionary<string, ILogger> m_loggersByName = new Dictionary<string, ILogger>();

        /// <summary>Constructor.</summary>
        public SerilogLogService()
        {
            // Initialise
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        #endregion

        #region ILogService implementation

        /// <summary>Creates a logger.</summary>
        public ILogger CreateLogger(string loggerName)
        {
            lock (m_loggersByName)
            {
                if (m_loggersByName.ContainsKey(loggerName))
                {
                    return m_loggersByName[loggerName];
                }
                ILogger newLogger = new SerilogLogger(loggerName);
                m_loggersByName.Add(loggerName, newLogger);
                return newLogger;
            }
        }

        /// <summary>Getter for the currently logged sessions.</summary>
        public IEnumerable<LogsModel.LogsSession> GetSessions()
        {
            return Array.Empty<LogsModel.LogsSession>();
        }

        /// <summary>Getter for the log entries for a session.</summary>
        public IEnumerable<LogsModel.LogsEntry> GetEntriesForSessionId(string sessionId)
        {
            return Array.Empty<LogsModel.LogsEntry>();
        }

        /// <summary>Getter for the entry detail.</summary>
        public LogsModel.LogEntryDetailJson GetEntryDetailForEntryId(int entryId)
        {
            return null;
        }

        #endregion
    }
}
