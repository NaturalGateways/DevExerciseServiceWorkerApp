using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.SqliteDatabases.MainDatabase
{
    public class MainDatabaseInitialiser : BaseDatabaseInitialiser
    {
        public override int MinVersion { get { return 1; } }

        public override int CurrentVersion { get { return 1; } }

        public static ISqliteConnection Connect()
        {
            MainDatabaseInitialiser initialiser = new MainDatabaseInitialiser();
            return initialiser.CreateConnection("Main.sqlite");
        }

        /// <summary>Sets up the database.</summary>
        protected override int SetupDatabase(ISqliteConnection connection)
        {
            // Create session table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS LOCAL_DOCUMENT (DOC_TYPE text NOT NULL, DOC_KEY text NOT NULL, JSON_DATA text NOT NULL, PRIMARY KEY (DOC_TYPE, DOC_KEY));", null);

            // Return current version
            return this.CurrentVersion;
        }
    }
}
