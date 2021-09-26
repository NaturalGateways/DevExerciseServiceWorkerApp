using System;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.Sync.PeriodicDownloadSteps
{
    public class JobStatusStep : IDownloadStep
    {
        #region IDownloadStep implementation

        /// <summary>Executes a download update.</summary>
        public void DownloadStep_ExecuteUpdate(ApiModel.LastMod lastMod)
        {
            // Check if we should sync
            DateTime? localLastMod = Services.MainDataService.GetRefListDateTime<DataModel.RefData.JobStatus>(null);
            DateTime? serverLastMod = lastMod?.RefData;
            if (serverLastMod.HasValue == false)
            {
                return;
            }
            if (localLastMod.HasValue && localLastMod.Value == serverLastMod.Value)
            {
                return;
            }

            // Download job statuses
            ApiModel.RefData.JobStatus[] apiData = Services.ApiService.GetJobStatuses();
            MainDataRefListItem<DataModel.RefData.JobStatus>[] localData = apiData.Select(x => new MainDataRefListItem<DataModel.RefData.JobStatus>
            {
                ItemKey = x.JobStatusId.ToString(),
                Item = new DataModel.RefData.JobStatus
                {
                    JobStatusId = x.JobStatusId,
                    JobStatusKey = x.JobStatusKey,
                    JobStatusName = x.JobStatusName
                }
            }).ToArray();
            Services.MainDataService.SetRefList<DataModel.RefData.JobStatus>(null, serverLastMod.Value, localData);
        }

        #endregion
    }
}
