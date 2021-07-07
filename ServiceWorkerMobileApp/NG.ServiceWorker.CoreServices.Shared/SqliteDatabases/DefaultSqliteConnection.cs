using System;
using System.Collections.Generic;

#if WINDOWS_UWP
using SqliteCommand = Microsoft.Data.Sqlite.SqliteCommand;
using SqliteConnection = Microsoft.Data.Sqlite.SqliteConnection;
using SqliteDataReader = Microsoft.Data.Sqlite.SqliteDataReader;
#else
using SqliteCommand = Mono.Data.Sqlite.SqliteCommand;
using SqliteConnection = Mono.Data.Sqlite.SqliteConnection;
using SqliteDataReader = Mono.Data.Sqlite.SqliteDataReader;
#endif

namespace NG.ServiceWorker.CoreServices.SqliteDatabases
{
    public class DefaultSqliteConnection : ISqliteConnection
    {
        #region Base

        /// <summary>The SQLite connection.</summary>
        private SqliteConnection m_connection = null;

        /// <summary>Constructor.</summary>
        public DefaultSqliteConnection(SqliteConnection connection)
        {
            m_connection = connection;
        }

        /// <summary>Creates a command.</summary>
        private SqliteCommand CreateCommand(string sql, Dictionary<string, object> bindVars)
        {
            SqliteCommand command = new SqliteCommand(sql, m_connection);
            if (bindVars != null)
            {
                foreach (KeyValuePair<string, object> bindVar in bindVars)
                {
                    command.Parameters.AddWithValue(bindVar.Key, bindVar.Value ?? DBNull.Value);
                }
            }
            return command;
        }

        #endregion

        #region ISqliteConnection : IDisposable implementation

        /// <summary>Dispose of the connection when done.</summary>
        public void Dispose()
        {
            if (m_connection != null)
            {
                m_connection.Close();
                m_connection.Dispose();
                m_connection = null;
            }
        }

        #endregion

        #region ISqliteConnection implementation

        /// <summary>Query implementation.</summary>
        public void Query(string sql, Dictionary<string, object> bindVars, Action<ISqliteRow> rowHandler)
        {
            lock (m_connection)
            {
                using (SqliteCommand command = CreateCommand(sql, bindVars))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DefaultSqliteRow row = new DefaultSqliteRow(reader);
                        while (reader.Read())
                        {
                            row.NewRow();
                            rowHandler.Invoke(row);
                        }
                    }
                }
            }
        }

        /// <summary>Queries if a single records exists.</summary>
        public bool QueryExists(string sql, Dictionary<string, object> bindVars)
        {
            lock (m_connection)
            {
                using (SqliteCommand command = CreateCommand(sql, bindVars))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        /// <summary>Queries a single integer.</summary>
        public int QueryInteger(string sql, Dictionary<string, object> bindVars, int? defaultValue = null)
        {
            lock (m_connection)
            {
                using (SqliteCommand command = CreateCommand(sql, bindVars))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.IsDBNull(0) == false)
                            {
                                return reader.GetInt32(0);
                            }
                        }
                        if (defaultValue.HasValue)
                        {
                            return defaultValue.Value;
                        }
                        throw new Exception("Integer query cannot return an integer.");
                    }
                }
            }
        }

        /// <summary>Queries a single JSON object.</summary>
        public JsonType QueryJson<JsonType>(string sql, Dictionary<string, object> bindVars)
        {
            lock (m_connection)
            {
                using (SqliteCommand command = CreateCommand(sql, bindVars))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.IsDBNull(0))
                            {
                                return default(JsonType);
                            }
                            return Services.JsonService.DeserialiseString<JsonType>(reader.GetString(0));
                        }
                        throw new Exception("JSON query has no records.");
                    }
                }
            }
        }

        /// <summary>Executes non-query SQL.</summary>
        public void ExecuteSql(string sql, Dictionary<string, object> bindVars)
        {
            lock (m_connection)
            {
                using (SqliteCommand command = CreateCommand(sql, bindVars))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
