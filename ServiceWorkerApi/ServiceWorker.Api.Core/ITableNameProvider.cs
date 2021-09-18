using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api
{
    public interface ITableNameProvider
    {
        /// <summary>Getter for a specific name of a table from a conceptual name.</summary>
        string GetInstanceNameFromConceptName(string conceptName);
    }
}
