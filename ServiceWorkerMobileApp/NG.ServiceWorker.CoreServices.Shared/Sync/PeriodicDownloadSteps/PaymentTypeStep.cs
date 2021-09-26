using System;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.Sync.PeriodicDownloadSteps
{
    public class PaymentTypeStep : IDownloadStep
    {
        #region IDownloadStep implementation

        /// <summary>Executes a download update.</summary>
        public void DownloadStep_ExecuteUpdate(ApiModel.LastMod lastMod)
        {
            // Check if we should sync
            DateTime? localLastMod = Services.MainDataService.GetRefListDateTime<DataModel.RefData.PaymentType>(null);
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
            ApiModel.RefData.PaymentType[] apiData = Services.ApiService.GetPaymentTypes();
            MainDataRefListItem<DataModel.RefData.PaymentType>[] localData = apiData.Select(x => new MainDataRefListItem<DataModel.RefData.PaymentType>
            {
                ItemKey = x.PaymentTypeId.ToString(),
                Item = new DataModel.RefData.PaymentType
                {
                    PaymentTypeId = x.PaymentTypeId,
                    PaymentTypeKey = x.PaymentTypeKey,
                    PaymentTypeName = x.PaymentTypeName
                }
            }).ToArray();
            Services.MainDataService.SetRefList<DataModel.RefData.PaymentType>(null, serverLastMod.Value, localData);
        }

        #endregion
    }
}
