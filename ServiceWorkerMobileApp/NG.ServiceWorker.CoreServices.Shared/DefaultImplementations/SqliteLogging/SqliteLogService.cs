using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.SqliteLogging
{
    public enum LogLevel
    {
        Trace = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }

    public class SqliteLogService : ILogService
    {
        #region Base

        /// <summary>The cached created loggers.</summary>
        private Dictionary<string, ILogger> m_loggersByName = new Dictionary<string, ILogger>();

        /// <summary>The SQLite connection.</summary>
        private ISqliteConnection m_connection = null;

        /// <summary>The SQLite connection.</summary>
        private string m_appSessionId = null;

        /// <summary>Constructor.</summary>
        public SqliteLogService()
        {
            // Connect to database
            m_connection = SqliteDatabases.LoggingDatabase.LoggingDatabaseInitialiser.Connect();

            // Create app session
            m_appSessionId = Guid.NewGuid().ToString();
            m_connection.ExecuteSql("INSERT INTO APP_SESSION (APP_SESSION_ID, START_DATETIME_UTC) VALUES (@appSessionId, @dateTimeUtc);", new System.Collections.Generic.Dictionary<string, object>
            {
                { "appSessionId", m_appSessionId },
                { "dateTimeUtc", DateTime.UtcNow }
            });
        }

        /// <summary>Inserts a log entry.</summary>
        public void InsertLogEntry(string loggerName, LogLevel logLevel, string message, Exception ex)
        {
            // Create log entry
            string sql = "INSERT INTO LOG_ENTRY (DATETIME_UTC, APP_SESSION_ID, COMPONENT_NAME, LOG_LEVEL_ID, MESSAGE, DETAIL_JSON) VALUES (@utcNow, @appSessionId, @compName, @logLevel, @message, @detailJson);";
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "utcNow", DateTime.UtcNow },
                { "appSessionId", m_appSessionId },
                { "compName", loggerName },
                { "logLevel", (int)logLevel },
                { "message", message },
                { "detailJson", null }
            };
            if (ex != null)
            {
                LogsModel.LogEntryDetailJson detail = new LogsModel.LogEntryDetailJson { ExceptionStack = new List<LogsModel.LogEntryDetailJsonException>() };
                while (ex != null)
                {
                    detail.ExceptionStack.Add(new LogsModel.LogEntryDetailJsonException
                    {
                        ExceptionType = ex.GetType().FullName,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    });
                    ex = ex.InnerException;
                }
                bindVars["detailJson"] = Services.JsonService.SerialiseObject(detail);
            }
            m_connection.ExecuteSql(sql, bindVars);
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
                ILogger newLogger = new SqliteLogger(this, loggerName);
                m_loggersByName.Add(loggerName, newLogger);
                return newLogger;
            }
        }

        /// <summary>Getter for the currently logged sessions.</summary>
        public IEnumerable<LogsModel.LogsSession> GetSessions()
        {
            List<LogsModel.LogsSession> sessionList = new List<LogsModel.LogsSession>();

            string sql = "SELECT app.APP_SESSION_ID, app.START_DATETIME_UTC, (SELECT COUNT(*) FROM LOG_ENTRY ent WHERE ent.APP_SESSION_ID=app.APP_SESSION_ID) FROM APP_SESSION app ORDER BY app.START_DATETIME_UTC DESC;";
            m_connection.Query(sql, null, (row) =>
            {
                string sessionId = row.GetString();
                DateTime startDateTimeUtc = row.GetDateTime();
                int entryNum = row.GetInteger();
                sessionList.Add(new LogsModel.LogsSession
                {
                    SessionId = sessionId,
                    StartDateTimeUtc = startDateTimeUtc,
                    EntryNum = entryNum
                });
            });

            return sessionList;
        }

        /// <summary>Getter for the log entries for a session.</summary>
        public IEnumerable<LogsModel.LogsEntry> GetEntriesForSessionId(string sessionId)
        {
            List<LogsModel.LogsEntry> entryList = new List<LogsModel.LogsEntry>();

            string[] sqlArray = new string[]
            {
                "SELECT ent.LOG_ENTRY_ID, ent.COMPONENT_NAME, ent.DATETIME_UTC, lvl.LOG_LEVEL_KEY, ent.MESSAGE,",
                "CASE WHEN (DETAIL_JSON IS NULL) THEN 0 ELSE 1 END",
                "FROM LOG_ENTRY ent",
                "INNER JOIN LOG_LEVEL lvl ON lvl.LOG_LEVEL_ID=ent.LOG_LEVEL_ID",
                "WHERE ent.APP_SESSION_ID=@sessionId;"
            };
            string sql = string.Join(" ", sqlArray);
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "sessionId", sessionId }
            };
            m_connection.Query(sql, bindVars, (row) =>
            {
                LogsModel.LogsEntry entry = new LogsModel.LogsEntry();
                entry.LogEntryId = row.GetInteger();
                entry.ComponentName = row.GetString();
                entry.DateTimeUtc = row.GetDateTime();
                entry.LogLevelName = row.GetString();
                entry.Message = row.GetString();
                entry.HasDetail = row.GetInteger() == 1;
                entryList.Add(entry);
            });

            return entryList;
        }

        /// <summary>Getter for the entry detail.</summary>
        public LogsModel.LogEntryDetailJson GetEntryDetailForEntryId(int entryId)
        {
            string sql = "SELECT DETAIL_JSON FROM LOG_ENTRY WHERE LOG_ENTRY_ID=@entryId;";
            Dictionary<string, object> bindVars = new Dictionary<string, object> { { "entryId", entryId } };
            return m_connection.QueryJson<LogsModel.LogEntryDetailJson>(sql, bindVars);
        }

        #endregion
    }
}
