using System;

namespace NG.ServiceWorker.CoreServices.Sync
{
    public interface IDownloadStep
    {
        /// <summary>Executes a download update.</summary>
        void DownloadStep_ExecuteUpdate(ApiModel.LastMod lastMod);
    }
}
