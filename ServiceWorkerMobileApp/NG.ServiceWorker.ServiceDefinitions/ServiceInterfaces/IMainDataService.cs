using System;

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
    }
}
