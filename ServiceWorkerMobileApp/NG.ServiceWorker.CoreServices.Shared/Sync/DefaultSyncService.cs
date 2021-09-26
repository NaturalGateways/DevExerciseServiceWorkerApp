using System;

namespace NG.ServiceWorker.CoreServices.Sync
{
    public class DefaultSyncService : ISyncService
    {
        #region Base

        /// <summary>A sync thread.</summary>
        private PeriodicDownloadSyncThread m_periodicDownloadSyncThread = null;

        #endregion

        #region ISyncService implementation

        /// <summary>Called by app setup to start the sync after initialisation.</summary>
        public void StartSync()
        {
            if (m_periodicDownloadSyncThread != null)
            {
                throw new Exception("Cannot double start sync threads.");
            }
            m_periodicDownloadSyncThread = new PeriodicDownloadSyncThread();
        }

        #endregion
    }
}
