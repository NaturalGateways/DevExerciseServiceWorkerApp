using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public enum EntityType { Contact = 1 }

    /// <summary>
    /// Service storing the main data of the application.
    /// </summary>
    public interface IMainDataService
    {
        /// <summary>Getter for a document.</summary>
        DocType GetDocument<DocType>(string docKey);

        /// <summary>Setter for a document.</summary>
        void SetDocument<DocType>(string docKey, DocType docObject);

        /// <summary>Setter for a document.</summary>
        DateTime? GetRefListDateTime<ItemType>(string listKey);

        /// <summary>Setter for a document.</summary>
        ItemType GetRefListItem<ItemType>(string listKey, string itemKey);

        /// <summary>Setter for a document.</summary>
        void SetRefList<ItemType>(string listKey, DateTime dateTime, IEnumerable<MainDataRefListItem<ItemType>> items);

        /// <summary>Fetches entity data.</summary>
        DataType GetEntityDataItem<DataType>(EntityType entityType, string entityId, string itemKey);

        /// <summary>Fetches all the entities, returning a data type for each entity.</summary>
        IEnumerable<DataType> GetEntitys<DataType>(EntityType entityType);

        /// <summary>Sets an entity in the data.</summary>
        void SetEntity(EntityType entityType, string entityId, IEnumerable<MainDataEntityDataItem> items);
    }
}
