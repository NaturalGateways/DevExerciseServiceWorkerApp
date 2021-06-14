using System;

using SqliteConnection = Mono.Data.Sqlite.SqliteConnection;

namespace NG.ServiceWorker.CoreServices.SqliteDatabases
{
    public class DefaultSqliteService : ISqliteService
    {
        #region Base

        #endregion

        #region ISqliteService implementation

        /// <summary>Connects to the database at the given location.</summary>
        public ISqliteConnection ConnectToFilepath(string filepath)
        {
            string connectionString = $"Data Source={filepath};Version=3;";
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
