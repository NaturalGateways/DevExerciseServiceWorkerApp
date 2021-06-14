using System;

namespace NG.ServiceWorker
{
    public interface ISqliteService
    {
        /// <summary>Connects to the database at the given location.</summary>
        ISqliteConnection ConnectToFilepath(string filepath);
    }
}
