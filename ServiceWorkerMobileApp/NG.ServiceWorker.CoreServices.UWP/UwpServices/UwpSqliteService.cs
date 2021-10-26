using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NG.ServiceWorker.CoreServices.UwpServices
{
    public class UwpSqliteService : SqliteDatabases.DefaultSqliteService
    {
        /// <summary>Creates a connection string fomr a filepath, overridable to allow changed depending on platform.</summary>
        protected override string CreateConnectionStringFromFilepath(string filepath)
        {
            return $"Data Source={filepath};";
        }
    }
}
