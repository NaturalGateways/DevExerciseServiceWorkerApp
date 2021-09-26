using System;

namespace NG.ServiceWorker
{
    public interface ISyncService
    {
        /// <summary>Called by app setup to start the sync after initialisation.</summary>
        void StartSync();
    }
}
