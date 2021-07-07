using System;

#if WINDOWS_UWP
using SqliteDataReader = Microsoft.Data.Sqlite.SqliteDataReader;
#else
using SqliteDataReader = Mono.Data.Sqlite.SqliteDataReader;
#endif

namespace NG.ServiceWorker.CoreServices.SqliteDatabases
{
    public class DefaultSqliteRow : ISqliteRow
    {
        #region Base

        private SqliteDataReader m_reader = null;

        private int m_columnIndex = 0;

        public DefaultSqliteRow(SqliteDataReader reader)
        {
            m_reader = reader;
        }

        public void NewRow()
        {
            m_columnIndex = 0;
        }

        #endregion

        #region ISqliteRow implementation

        /// <summary>Getter for a not-nullable string.</summary>
        public string GetString()
        {
            int columnIndex = m_columnIndex++;
            if (m_reader.IsDBNull(columnIndex))
            {
                throw new Exception("Expected a not-null string from a SQLite database.");
            }
            return m_reader.GetString(columnIndex);
        }

        /// <summary>Getter for a not-nullable datetime.</summary>
        public DateTime GetDateTime()
        {
            int columnIndex = m_columnIndex++;
            if (m_reader.IsDBNull(columnIndex))
            {
                throw new Exception("Expected a not-null date time from a SQLite database.");
            }
            return m_reader.GetDateTime(columnIndex);
        }

        /// <summary>Getter for a not-nullable integer.</summary>
        public int GetInteger()
        {
            int columnIndex = m_columnIndex++;
            if (m_reader.IsDBNull(columnIndex))
            {
                throw new Exception("Expected a not-null integer from a SQLite database.");
            }
            return m_reader.GetInt32(columnIndex);
        }

        #endregion
    }
}
