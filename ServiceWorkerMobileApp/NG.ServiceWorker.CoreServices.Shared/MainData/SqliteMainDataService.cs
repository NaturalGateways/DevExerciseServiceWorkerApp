using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.MainData
{
    public class SqliteMainDataService : IMainDataService
    {
        #region Base

        /// <summary>The SQLite connection.</summary>
        private ISqliteConnection m_connection = null;

        /// <summary>Constructor.</summary>
        public SqliteMainDataService()
        {
            // Connect to database
            m_connection = SqliteDatabases.MainDatabase.MainDatabaseInitialiser.Connect();
        }

        #endregion

        #region IMainDataService implementation

        /// <summary>Getter for a document.</summary>
        public DocType GetDocument<DocType>(string docKey)
        {
            // Get the type
            DocType docType = default(DocType);
            string sql = "SELECT JSON_DATA FROM LOCAL_DOCUMENT WHERE DOC_TYPE=@docType AND DOC_KEY=@docKey LIMIT 1;";
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "docType", typeof(DocType).FullName },
                { "docKey", docKey ?? "NO_KEY" }
            };
            m_connection.Query(sql, bindVars, (row) =>
            {
                docType = Services.JsonService.DeserialiseString<DocType>(row.GetString());
            });
            return docType;
        }

        /// <summary>Setter for a document.</summary>
        public void SetDocument<DocType>(string docKey, DocType docObject)
        {
            // Create the SQL
            string sql = "INSERT INTO LOCAL_DOCUMENT (DOC_TYPE, DOC_KEY, JSON_DATA) VALUES (@docType, @docKey, @jsonData) ON CONFLICT(DOC_TYPE, DOC_KEY) DO UPDATE SET JSON_DATA=@jsonData WHERE DOC_TYPE=@docType AND DOC_KEY=@docKey;";
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "docType", typeof(DocType).FullName },
                { "docKey", docKey ?? "NO_KEY" },
                { "jsonData", Services.JsonService.SerialiseObject(docObject) }
            };
            m_connection.ExecuteSql(sql, bindVars);
        }

        #endregion
    }
}
