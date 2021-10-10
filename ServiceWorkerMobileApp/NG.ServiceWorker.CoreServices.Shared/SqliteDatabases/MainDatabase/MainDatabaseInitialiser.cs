using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.SqliteDatabases.MainDatabase
{
    public class MainDatabaseInitialiser : BaseDatabaseInitialiser
    {
        public override int MinVersion { get { return 1; } }

        public override int CurrentVersion { get { return 3; } }

        public static ISqliteConnection Connect()
        {
            MainDatabaseInitialiser initialiser = new MainDatabaseInitialiser();
            return initialiser.CreateConnection("Main.sqlite");
        }

        /// <summary>Sets up the database.</summary>
        protected override int SetupDatabase(ISqliteConnection connection)
        {
            // Create local document table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS LOCAL_DOCUMENT (DOC_TYPE text NOT NULL, DOC_KEY text NOT NULL, JSON_DATA text NOT NULL, PRIMARY KEY (DOC_TYPE, DOC_KEY));", null);

            // Create reference list table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS REF_LIST (REF_TYPE text NOT NULL, LIST_KEY text NOT NULL, DATETIME_UTC text NOT NULL, PRIMARY KEY (REF_TYPE, LIST_KEY));", null);

            // Create reference item table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS REF_ITEM (REF_TYPE text NOT NULL, LIST_KEY text NOT NULL, ITEM_KEY text NOT NULL, JSON_DATA text NOT NULL, PRIMARY KEY (REF_TYPE, LIST_KEY, ITEM_KEY));", null);
            connection.ExecuteSql("CREATE INDEX IDX_REF_ITEM_BY_LIST ON REF_ITEM (REF_TYPE, LIST_KEY);", null);

            // Create entity table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS ENTITY (ENTITY_TYPE integer NOT NULL, ENTITY_ID text NOT NULL, PRIMARY KEY (ENTITY_TYPE, ENTITY_ID));", null);

            // Create entity data table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS ENTITY_DATA (ENTITY_TYPE integer NOT NULL, ENTITY_ID text NOT NULL, DATA_TYPE text NOT NULL, DATA_KEY text NOT NULL, JSON_DATA text NOT NULL, PRIMARY KEY (ENTITY_TYPE, ENTITY_ID, DATA_TYPE, DATA_KEY));", null);
            connection.ExecuteSql("CREATE INDEX IDX_ENTITY_DATA_BY_ENTITY ON ENTITY_DATA (ENTITY_TYPE, ENTITY_ID, DATA_TYPE);", null);

            // Return current version
            return this.CurrentVersion;
        }

        /// <summary>Sets up the database.</summary>
        protected override int UpgradeDatabase(ISqliteConnection connection, int curVersion)
        {
            switch (curVersion)
            {
                case 1:
                    return Upgrade1to2_AddRefListTables(connection);
                case 2:
                    return Upgrade2to3_AddentityTables(connection);
                default:
                    throw new Exception($"No code to handle an upgrade from {curVersion}.");
            }
        }

        /// <summary>Sets up the database.</summary>
        private int Upgrade1to2_AddRefListTables(ISqliteConnection connection)
        {
            // Create reference list table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS REF_LIST (REF_TYPE text NOT NULL, LIST_KEY text NOT NULL, DATETIME_UTC text NOT NULL, PRIMARY KEY (REF_TYPE, LIST_KEY));", null);

            // Create reference item table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS REF_ITEM (REF_TYPE text NOT NULL, LIST_KEY text NOT NULL, ITEM_KEY text NOT NULL, JSON_DATA text NOT NULL, PRIMARY KEY (REF_TYPE, LIST_KEY, ITEM_KEY));", null);
            connection.ExecuteSql("CREATE INDEX IDX_REF_ITEM_BY_LIST ON REF_ITEM (REF_TYPE, LIST_KEY);", null);

            // Return
            return 2;
        }

        /// <summary>Sets up the database.</summary>
        private int Upgrade2to3_AddentityTables(ISqliteConnection connection)
        {
            // Create entity table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS ENTITY (ENTITY_TYPE integer NOT NULL, ENTITY_ID text NOT NULL, PRIMARY KEY (ENTITY_TYPE, ENTITY_ID));", null);

            // Create entity data table
            connection.ExecuteSql("CREATE TABLE IF NOT EXISTS ENTITY_DATA (ENTITY_TYPE integer NOT NULL, ENTITY_ID text NOT NULL, DATA_TYPE text NOT NULL, DATA_KEY text NOT NULL, JSON_DATA text NOT NULL, PRIMARY KEY (ENTITY_TYPE, ENTITY_ID, DATA_TYPE, DATA_KEY));", null);
            connection.ExecuteSql("CREATE INDEX IDX_ENTITY_DATA_BY_ENTITY ON ENTITY_DATA (ENTITY_TYPE, ENTITY_ID, DATA_TYPE);", null);

            // Return
            return 3;
        }
    }
}
