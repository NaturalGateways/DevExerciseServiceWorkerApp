using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.Sync.PeriodicDownloadSteps
{
    public class FormsStep : IDownloadStep
    {
        #region IDownloadStep implementation

        /// <summary>Executes a download update.</summary>
        public void DownloadStep_ExecuteUpdate(ApiModel.LastMod lastMod)
        {
            IApiService apiService = Services.ApiService;

            // Check if we should sync
            DateTime? localLastMod = Services.MainDataService.GetRefListDateTime<DataModel.FormDesign>(null);
            DateTime? serverLastMod = lastMod?.RefData;
            if (serverLastMod.HasValue == false)
            {
                return;
            }
            if (localLastMod.HasValue && localLastMod.Value == serverLastMod.Value)
            {
                return;
            }

            // Download summaries
            ApiModel.FormDesignSummary formsSummary = apiService.GetFormDesigns();

            // Traverse forms, creating the list
            List<MainDataRefListItem<DataModel.FormDesign>> localData = new List<MainDataRefListItem<DataModel.FormDesign>>();
            foreach (string formKey in formsSummary.forms)
            {
                // Fetch form
                ApiModel.FormDesign apiDesign = apiService.GetFormDesign(formKey);

                // Create data model
                DataModel.FormDesign dataDesign = new DataModel.FormDesign
                {
                    FormType = apiDesign.FormType,
                    FormKey = apiDesign.Key
                };

                // Fetch Forms IO
                if (dataDesign.FormType == "FormsIO")
                {
                    dataDesign.FormsIO = apiService.GetFormsIOModel(apiDesign.Url);
                }

                // Add to list
                localData.Add(new MainDataRefListItem<DataModel.FormDesign>
                {
                    ItemKey = dataDesign.FormKey,
                    Item = dataDesign
                });
            }

            // Store data
            Services.MainDataService.SetRefList<DataModel.FormDesign>(null, serverLastMod.Value, localData);
        }

        #endregion
    }
}
