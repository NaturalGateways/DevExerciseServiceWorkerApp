using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.MainData
{
    public class SqliteMainDataService : IMainDataService
    {
        #region Base

        /// <summary>The SQLite connection.</summary>
        private ISqliteConnection m_connection = null;

        /// <summary>Mutex for the reference data read and write.</summary>
        private object m_refDataLock = new object();
        /// <summary>Mutex for the entity data read and write.</summary>
        private object m_entityLock = new object();

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

        /// <summary>Setter for a document.</summary>
        public DateTime? GetRefListDateTime<ItemType>(string listKey)
        {
            DateTime? dateTime = null;
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "refType", typeof(ItemType).FullName },
                { "listKey", listKey ?? "NO_KEY" }
            };
            lock (m_refDataLock)
            {
                m_connection.Query("SELECT DATETIME_UTC FROM REF_LIST WHERE REF_TYPE=@refType AND LIST_KEY=@listKey LIMIT 1;", bindVars, (row) =>
                {
                    dateTime = row.GetDateTime();
                });
            }
            return dateTime;
        }

        /// <summary>Setter for a document.</summary>
        public ItemType GetRefListItem<ItemType>(string listKey, string itemKey)
        {
            string refType = typeof(ItemType).FullName;
            listKey = listKey ?? "NO_KEY";
            itemKey = itemKey ?? "NO_KEY";

            // Get item
            ItemType itemType = default(ItemType);
            lock (m_refDataLock)
            {
                Dictionary<string, object> bindVars = new Dictionary<string, object>
                {
                    { "refType", refType },
                    { "listKey", listKey },
                    { "itemKey", itemKey }
                };
                m_connection.Query("SELECT JSON_DATA FROM REF_ITEM WHERE REF_TYPE=@refType AND LIST_KEY=@listKey AND ITEM_KEY=@itemKey LIMIT 1;", bindVars, (row) =>
                {
                    itemType = Services.JsonService.DeserialiseString<ItemType>(row.GetString());
                });
            }
            return itemType;
        }

        /// <summary>Setter for a document.</summary>
        public void SetRefList<ItemType>(string listKey, DateTime dateTime, IEnumerable<MainDataRefListItem<ItemType>> items)
        {
            string refType = typeof(ItemType).FullName;
            listKey = listKey ?? "NO_KEY";

            lock (m_refDataLock)
            {
                // Delete existing items
                {
                    Dictionary<string, object> bindVars = new Dictionary<string, object>
                    {
                        { "refType", refType },
                        { "listKey", listKey }
                    };
                    m_connection.ExecuteSql("DELETE FROM REF_ITEM WHERE REF_TYPE=@refType AND LIST_KEY=@listKey;", bindVars);
                }

                // Insert new items
                {
                    Dictionary<string, object> bindVars = new Dictionary<string, object>
                    {
                        { "refType", refType },
                        { "listKey", listKey },
                        { "itemKey", null },
                        { "jsonData", null }
                    };
                    foreach (MainDataRefListItem<ItemType> item in items)
                    {
                        bindVars["itemKey"] = item.ItemKey ?? "NO_KEY";
                        bindVars["jsonData"] = Services.JsonService.SerialiseObject(item.Item);
                        m_connection.ExecuteSql("INSERT INTO REF_ITEM (REF_TYPE, LIST_KEY, ITEM_KEY, JSON_DATA) VALUES (@refType, @listKey, @itemKey, @jsonData);", bindVars);
                    }
                }

                // Upsert ref list
                {
                    string sql = "INSERT INTO REF_LIST (REF_TYPE, LIST_KEY, DATETIME_UTC) VALUES (@refType, @listKey, @dateTime) ON CONFLICT(REF_TYPE, LIST_KEY) DO UPDATE SET DATETIME_UTC=@dateTime WHERE REF_TYPE=@refType AND LIST_KEY=@listKey;";
                    Dictionary<string, object> bindVars = new Dictionary<string, object>
                    {
                        { "refType", refType },
                        { "listKey", listKey },
                        { "dateTime", dateTime }
                    };
                    m_connection.ExecuteSql(sql, bindVars);
                }
            }
        }

        /// <summary>Fetches entity data.</summary>
        public DataType GetEntityDataItem<DataType>(EntityType entityType, string entityId, string itemKey)
        {
            string sql = "SELECT JSON_DATA FROM ENTITY_DATA WHERE ENTITY_TYPE=@entityType AND ENTITY_ID=@entityId AND DATA_TYPE=@dataType AND DATA_KEY=@dataKey LIMIT 1;";
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "entityType", (int)entityType },
                { "entityId", entityId },
                { "dataType", typeof(DataType).FullName },
                { "dataKey", itemKey ?? "NO_KEY" }
            };
            lock (m_entityLock)
            {
                DataType dataType = default(DataType);
                m_connection.Query(sql, bindVars, (row) =>
                {
                    dataType = row.GetJson<DataType>();
                });
                return dataType;
            }
        }

        /// <summary>Fetches all the entities, returning a data type for each entity.</summary>
        public IEnumerable<DataType> GetEntitys<DataType>(EntityType entityType)
        {
            string sql = "SELECT JSON_DATA FROM ENTITY_DATA WHERE ENTITY_TYPE=@entityType AND DATA_TYPE=@dataType AND DATA_KEY='NO_KEY';";
            Dictionary<string, object> bindVars = new Dictionary<string, object>
            {
                { "entityType", (int)entityType },
                { "dataType", typeof(DataType).FullName }
            };
            lock (m_entityLock)
            {
                List<DataType> dataList = new List<DataType>();
                m_connection.Query(sql, bindVars, (row) =>
                {
                    dataList.Add(row.GetJson<DataType>());
                });
                return dataList;
            }
        }

        /// <summary>Sets an entity in the data.</summary>
        public void SetEntity(EntityType entityType, string entityId, IEnumerable<MainDataEntityDataItem> items)
        {
            int entityTypeInt = (int)entityType;

            lock (m_entityLock)
            {
                // Check exists
                const string existsSql = "SELECT 1 FROM ENTITY WHERE ENTITY_TYPE=@entityType AND ENTITY_ID=@entityId LIMIT 1;";
                Dictionary<string, object> existsBindVars = new Dictionary<string, object>
                {
                    { "entityType", entityTypeInt },
                    { "entityId", entityId }
                };
                if (m_connection.QueryExists(existsSql, existsBindVars))
                {
                    // Insert or update components
                    if (items?.Any() ?? false)
                    {
                        const string itemCheckSql = "SELECT 1 FROM ENTITY_DATA WHERE ENTITY_TYPE=@entityType AND ENTITY_ID=@entityId AND DATA_TYPE=@dataType AND DATA_KEY=@dataKey LIMIT 1;";
                        const string itemInsertSql = "INSERT INTO ENTITY_DATA (ENTITY_TYPE, ENTITY_ID, DATA_TYPE, DATA_KEY, JSON_DATA) VALUES (@entityType, @entityId, @dataType, @dataKey, @jsonData);";
                        const string itemUpdateSql = "UPDATE ENTITY_DATA SET JSON_DATA=@jsonData WHERE ENTITY_TYPE=@entityType AND ENTITY_ID=@entityId AND DATA_TYPE=@dataType AND DATA_KEY=@dataKey;";
                        foreach (MainDataEntityDataItem item in items)
                        {
                            Dictionary<string, object> itemCheckBindVars = new Dictionary<string, object>
                            {
                                { "entityType", entityTypeInt },
                                { "entityId", entityId },
                                { "dataType", item.DataType.FullName },
                                { "dataKey", item.DataKey ?? "NO_KEY" }
                            };
                            Dictionary<string, object> itemSetBindVars = new Dictionary<string, object>
                            {
                                { "entityType", entityTypeInt },
                                { "entityId", entityId },
                                { "dataType", item.DataType.FullName },
                                { "dataKey", item.DataKey ?? "NO_KEY" },
                                { "jsonData", Services.JsonService.SerialiseObject(item.DataObject) }
                            };
                            if (m_connection.QueryExists(itemCheckSql, itemCheckBindVars))
                            {
                                m_connection.ExecuteSql(itemUpdateSql, itemSetBindVars);
                            }
                            else
                            {
                                m_connection.ExecuteSql(itemInsertSql, itemSetBindVars);
                            }
                        }
                    }
                }
                else
                {
                    // Insert entity
                    const string entitySql = "INSERT INTO ENTITY (ENTITY_TYPE, ENTITY_ID) VALUES (@entityType, @entityId);";
                    m_connection.ExecuteSql(entitySql, existsBindVars);

                    // Insert components
                    if (items?.Any() ?? false)
                    {
                        const string entityDataSql = "INSERT INTO ENTITY_DATA (ENTITY_TYPE, ENTITY_ID, DATA_TYPE, DATA_KEY, JSON_DATA) VALUES (@entityType, @entityId, @dataType, @dataKey, @jsonData);";
                        foreach (MainDataEntityDataItem item in items)
                        {
                            Dictionary<string, object> entityDataBindVars = new Dictionary<string, object>
                            {
                                { "entityType", entityTypeInt },
                                { "entityId", entityId },
                                { "dataType", item.DataType.FullName },
                                { "dataKey", item.DataKey ?? "NO_KEY" },
                                { "jsonData", Services.JsonService.SerialiseObject(item.DataObject) }
                            };
                            m_connection.ExecuteSql(entityDataSql, entityDataBindVars);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
