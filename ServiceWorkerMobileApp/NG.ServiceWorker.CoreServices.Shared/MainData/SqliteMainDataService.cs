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
            // Get the type
            string checkSql = "SELECT 1 FROM LOCAL_DOCUMENT WHERE DOC_TYPE=@docType AND DOC_KEY=@docKey LIMIT 1;";
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "docType", typeof(DocType).FullName },
                { "docKey", docKey ?? "NO_KEY" }
            };
            if (m_connection.QueryExists(checkSql, bindVars))
            {
                string updateSql = "UPDATE LOCAL_DOCUMENT SET JSON_DATA=@jsonData WHERE DOC_TYPE=@docType AND DOC_KEY=@docKey;";
                bindVars["jsonData"] = Services.JsonService.SerialiseObject(docObject);
                m_connection.ExecuteSql(updateSql, bindVars);
            }
            else
            {
                string updateSql = "INSERT INTO LOCAL_DOCUMENT (DOC_TYPE, DOC_KEY, JSON_DATA) VALUES (@docType, @docKey, @jsonData);";
                bindVars["jsonData"] = Services.JsonService.SerialiseObject(docObject);
                m_connection.ExecuteSql(updateSql, bindVars);
            }
        }

        #endregion
    }
}
