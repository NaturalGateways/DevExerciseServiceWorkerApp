using System;

namespace NG.ServiceWorker
{
    public interface IFormsService
    {
        /// <summary>Creates a form from a design.</summary>
        SwForms.IFormDocument CreateFormFromDesign(DataModel.FormDesign formDesign);

        /// <summary>Creates the form instance from the form.</summary>
        object ConvertFormToFormInstance(SwForms.IFormDocument document);

        /// <summary>Creates a job form.</summary>
        SwForms.IFormDocument CreateJobForm(object formData);

        /// <summary>Creates the job data from a form.</summary>
        object ConvertJobFormToData(SwForms.IFormDocument document);

        /// <summary>Creates a form for editing a contact.</summary>
        SwForms.IFormDocument CreateContactEditForm(string contactId);
    }
}
