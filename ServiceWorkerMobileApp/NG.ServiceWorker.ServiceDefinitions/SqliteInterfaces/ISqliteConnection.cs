using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public interface ISqliteConnection : IDisposable
    {
        /// <summary>Query implementation.</summary>
        void Query(string sql, Dictionary<string, object> bindVars, Action<ISqliteRow> rowHandler);

        /// <summary>Queries if a single records exists.</summary>
        bool QueryExists(string sql, Dictionary<string, object> bindVars);

        /// <summary>Queries a single integer.</summary>
        int QueryInteger(string sql, Dictionary<string, object> bindVars, int? defaultValue=null);

        /// <summary>Queries a single JSON object.</summary>
        JsonType QueryJson<JsonType>(string sql, Dictionary<string, object> bindVars);

        /// <summary>Executes non-query SQL.</summary>
        void ExecuteSql(string sql, Dictionary<string, object> bindVars);
    }
}
