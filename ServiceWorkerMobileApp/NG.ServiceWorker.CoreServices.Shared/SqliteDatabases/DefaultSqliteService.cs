using System;

#if WINDOWS_UWP
using SqliteConnection = Microsoft.Data.Sqlite.SqliteConnection;
#else
using SqliteConnection = Mono.Data.Sqlite.SqliteConnection;
#endif

namespace NG.ServiceWorker.CoreServices.SqliteDatabases
{
    public class DefaultSqliteService : ISqliteService
    {
        #region Base

        /// <summary>Creates a connection string fomr a filepath, overridable to allow changed depending on platform.</summary>
        protected virtual string CreateConnectionStringFromFilepath(string filepath)
        {
            return $"Data Source={filepath};Version=3;";
        }

        #endregion

        #region ISqliteService implementation

        /// <summary>Connects to the database at the given location.</summary>
        public ISqliteConnection ConnectToFilepath(string filepath)
        {
            string connectionString = CreateConnectionStringFromFilepath(filepath);
            SqliteConnection conn = new SqliteConnection(connectionString);
            conn.Open();

            // Don't backup database
            Services.FileSystemService.SetFileBackedup(filepath, false);

            // Return
            return new DefaultSqliteConnection(conn);
        }

        #endregion
    }
}
