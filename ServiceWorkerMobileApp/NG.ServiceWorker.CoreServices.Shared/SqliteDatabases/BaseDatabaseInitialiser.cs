using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.SqliteDatabases
{
    public abstract class BaseDatabaseInitialiser
    {
        public abstract int MinVersion { get; }

        public abstract int CurrentVersion { get; }

        public ISqliteConnection CreateConnection(string filename)
        {
            // Create database
            string filepath = Services.FileService.GetDatabaseFilepath(filename);
            ISqliteConnection connection = Services.SqliteService.ConnectToFilepath(filepath);

            // Get database version
            int databaseVersion = 0;
            if (connection.QueryExists("SELECT 1 FROM sqlite_master WHERE TYPE='table' AND NAME='DB_VERSION';", null))
            {
                databaseVersion = connection.QueryInteger("SELECT VERSION FROM DB_VERSION;", null, 0);
            }
            else
            {
                connection.ExecuteSql("CREATE TABLE IF NOT EXISTS DB_VERSION (VERSION numeric NOT NULL);", null);
            }

            // Check if we initialise
            if (databaseVersion == 0)
            {
                // Setup
                databaseVersion = SetupDatabase(connection);
                // Set new version
                Dictionary<string, object> updateVersionBindVars = new Dictionary<string, object> { { "version", databaseVersion } };
                if (connection.QueryExists("SELECT 1 FROM DB_VERSION;", null))
                {
                    connection.ExecuteSql("UPDATE DB_VERSION SET VERSION=@version;", updateVersionBindVars);
                }
                else
                {
                    connection.ExecuteSql("INSERT INTO DB_VERSION (VERSION) VALUES (@version);", updateVersionBindVars);
                }
            }

            // Check if we upgrade
            while (databaseVersion < this.CurrentVersion)
            {
                databaseVersion = UpgradeDatabase(connection, databaseVersion);

                Dictionary<string, object> updateVersionBindVars = new Dictionary<string, object> { { "version", databaseVersion } };
                connection.ExecuteSql("UPDATE DB_VERSION SET VERSION=@version;", updateVersionBindVars);
            }

            // Return
            return connection;
        }

        /// <summary>Sets up the database.</summary>
        protected abstract int SetupDatabase(ISqliteConnection connection);

        /// <summary>Sets up the database.</summary>
        protected virtual int UpgradeDatabase(ISqliteConnection connection, int curVersion)
        {
            throw new Exception("This type of database doesn't current support upgrades.");
        }
    }
}
