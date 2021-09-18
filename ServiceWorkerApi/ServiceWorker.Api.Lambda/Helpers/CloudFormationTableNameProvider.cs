using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api.Lambda.Helpers
{
    public class CloudFormationTableNameProvider
    {
        /// <summary>Getter for a specific name of a table from a conceptual name.</summary>
        public string GetInstanceNameFromConceptName(string conceptName)
        {
            string environmentName = $"DbTable{conceptName}";
            string environmentValue = Environment.GetEnvironmentVariable(environmentName);
            return environmentValue;
        }
    }
}
