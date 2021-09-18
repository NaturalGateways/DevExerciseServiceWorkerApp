using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.SandboxConsole.Helpers
{
    public class SandboxTableNameProvider : Api.ITableNameProvider
    {
        /// <summary>Getter for a specific name of a table from a conceptual name.</summary>
        public string GetInstanceNameFromConceptName(string conceptName)
        {
            return $"ServiceWorker{conceptName}";
        }
    }
}
