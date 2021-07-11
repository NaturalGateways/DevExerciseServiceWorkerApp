using System;

namespace NG.ServiceWorker
{
    public interface IFormsService
    {
        /// <summary>Creates a job form.</summary>
        SwForms.IFormDocument CreateJobForm();
    }
}
