using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
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
    }
}
