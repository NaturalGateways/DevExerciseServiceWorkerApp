using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api.TableNameProviders
{
    public class StubTableNameProvider : ITableNameProvider
    {
        /// <summary>Getter for a specific name of a table from a conceptual name.</summary>
        public string GetInstanceNameFromConceptName(string conceptName)
        {
            return conceptName;
        }
    }
}
