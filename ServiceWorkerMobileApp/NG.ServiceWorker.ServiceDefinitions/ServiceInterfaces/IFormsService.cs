using System;

namespace NG.ServiceWorker
{
    public interface IFormsService
    {
        /// <summary>Creates a job form.</summary>
        SwForms.IFormDocument CreateJobForm(object formData);

        /// <summary>Creates the job data from a form.</summary>
        object ConvertJobFormToData(SwForms.IFormDocument document);
    }
}
