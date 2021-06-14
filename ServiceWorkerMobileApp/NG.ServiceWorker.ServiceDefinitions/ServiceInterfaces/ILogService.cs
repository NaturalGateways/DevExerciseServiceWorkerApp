using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public interface ILogger
    {
        void Trace(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        void Warn(string message, Exception ex);
        void Error(string message, Exception ex);
        void Fatal(string message, Exception ex);
    }

    public interface ILogService
    {
        /// <summary>Creates a logger.</summary>
        ILogger CreateLogger(string loggerName);

        /// <summary>Getter for the currently logged sessions.</summary>
        IEnumerable<LogsModel.LogsSession> GetSessions();

        /// <summary>Getter for the log entries for a session.</summary>
        IEnumerable<LogsModel.LogsEntry> GetEntriesForSessionId(string sessionId);

        /// <summary>Getter for the entry detail.</summary>
        LogsModel.LogEntryDetailJson GetEntryDetailForEntryId(int entryId);
    }
}
