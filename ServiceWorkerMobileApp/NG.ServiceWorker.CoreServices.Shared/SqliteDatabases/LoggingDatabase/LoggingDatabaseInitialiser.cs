using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.SqliteDatabases.LoggingDatabase
{
    public class LoggingDatabaseInitialiser
    {
        public const int MIN_VERSION = 1;

        public const int CURRENT_VERSION = 1;

        public static ISqliteConnection Connect()
        {
            // Create database
            string filepath = Services.FileService.GetDatabaseFilepath("Logs.sqlite");
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

            // Return
            return connection;
        }

        /// <summary>Sets up the database.</summary>
        private static int SetupDatabase(ISqliteConnection connection)
        {
            // Create session table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS APP_SESSION (APP_SESSION_ID text NOT NULL PRIMARY KEY, START_DATETIME_UTC text NOT NULL);", null);

            // Create log level table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS LOG_LEVEL (LOG_LEVEL_ID integer NOT NULL PRIMARY KEY, LOG_LEVEL_KEY varchar(5) NOT NULL, LOG_LEVEL_NAME varchar(5) NOT NULL);", null);
            connection.ExecuteSql("INSERT INTO LOG_LEVEL (LOG_LEVEL_ID, LOG_LEVEL_KEY, LOG_LEVEL_NAME) VALUES (1, 'TRACE', 'Trace');", null);
            connection.ExecuteSql("INSERT INTO LOG_LEVEL (LOG_LEVEL_ID, LOG_LEVEL_KEY, LOG_LEVEL_NAME) VALUES (2, 'INFO', 'Info');", null);
            connection.ExecuteSql("INSERT INTO LOG_LEVEL (LOG_LEVEL_ID, LOG_LEVEL_KEY, LOG_LEVEL_NAME) VALUES (3, 'WARN', 'Warn');", null);
            connection.ExecuteSql("INSERT INTO LOG_LEVEL (LOG_LEVEL_ID, LOG_LEVEL_KEY, LOG_LEVEL_NAME) VALUES (4, 'ERROR', 'Error');", null);
            connection.ExecuteSql("INSERT INTO LOG_LEVEL (LOG_LEVEL_ID, LOG_LEVEL_KEY, LOG_LEVEL_NAME) VALUES (5, 'FATAL', 'Fatal');", null);

            // Create log entry table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS LOG_ENTRY (LOG_ENTRY_ID integer NOT NULL PRIMARY KEY AUTOINCREMENT, DATETIME_UTC text NOT NULL, UPLOAD_STATUS char DEFAULT 'C' NOT NULL, APP_SESSION_ID text NOT NULL, COMPONENT_NAME text NOT NULL, LOG_LEVEL_ID number NOT NULL, MESSAGE text NOT NULL, DETAIL_JSON text NULL);", null);
            connection.ExecuteSql("CREATE INDEX IDX_LOG_ENTRY_BY_SESSION ON LOG_ENTRY (APP_SESSION_ID, DATETIME_UTC);", null);

            // Return current version
            return CURRENT_VERSION;
        }
    }
}
