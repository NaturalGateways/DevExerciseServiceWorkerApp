using System;

namespace NG.ServiceWorker
{
    public interface IApiService
    {
        /// <summary>Get for the last modified date times from the server.</summary>
        ApiModel.LastMod GetLastModifiedDateTimes();

        /// <summary>Geter for the form designs.</summary>
        ApiModel.FormDesignSummary GetFormDesigns();
        /// <summary>Geter for a specific form.</summary>
        ApiModel.FormDesign GetFormDesign(string formKey);
        /// <summary>Get a Forms.IO design.</summary>
        ApiModel.FormIOModel.FormDesign GetFormsIOModel(string url);

        /// <summary>Get for some reference data.</summary>
        ApiModel.RefData.JobStatus[] GetJobStatuses();
        /// <summary>Get for some reference data.</summary>
        ApiModel.RefData.PaymentType[] GetPaymentTypes();
        /// <summary>Get for some reference data.</summary>
        ApiModel.RefData.MaterialType[] GetMaterialTypes();

        /// <summary>Get the jobs.</summary>
        ApiModel.JobWithLinks[] GetJobArray();

        /// <summary>Get the form design of a job.</summary>
        ApiModel.FormIOModel.FormDesign GetJobFormDesign();
    }
}
