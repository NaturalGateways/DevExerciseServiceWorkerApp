using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultFormsServices : IFormsService
    {
        #region IFormsService implementation

        /// <summary>Creates a form from a design.</summary>
        public SwForms.IFormDocument CreateFormFromDesign(DataModel.FormDesign formDesign)
        {
            return FormTypes.FormsIO.Converter.ApiModel2SwForms.CreateDocument(formDesign.FormsIO);
        }

        /// <summary>Creates the form instance from the form.</summary>
        public object ConvertFormToFormInstance(SwForms.IFormDocument document)
        {
            return FormTypes.FormsIO.Converter.SwForms2DataModel.CreateData(document);
        }

        /// <summary>Creates a job form.</summary>
        public SwForms.IFormDocument CreateJobForm(object formData)
        {
            ApiModel.FormIOModel.FormDesign formDesign = Services.ApiService.GetJobFormDesign();
            FormTypes.FormsIO.FormsIoDocument formDocument = FormTypes.FormsIO.Converter.ApiModel2SwForms.CreateDocument(formDesign);
            if (formData != null)
            {
                FormTypes.FormsIO.Converter.LoadDataModel.Load(formData, formDocument);
            }
            return formDocument;
        }

        /// <summary>Creates the job data from a form.</summary>
        public object ConvertJobFormToData(SwForms.IFormDocument document)
        {
            return FormTypes.FormsIO.Converter.SwForms2DataModel.CreateData(document);
        }

        /// <summary>Creates a form for editing a contact.</summary>
        public SwForms.IFormDocument CreateContactEditForm(string contactId)
        {
            // Load the contact
            DataModel.Contact contact = Services.MainDataService.GetEntityDataItem<DataModel.Contact>(EntityType.Contact, contactId, null);
            if (contact == null)
            {
                throw new Exception($"Cannot find contact '{contactId}',");
            }

            // Load contact form
            DataModel.FormDesign formDesign = Services.MainDataService.GetRefListItem<DataModel.FormDesign>(null, "contact");
            FormTypes.FormsIO.FormsIoDocument formDocument = FormTypes.FormsIO.Converter.ApiModel2SwForms.CreateDocument(formDesign.FormsIO);
            if (contact.Data != null)
            {
                FormTypes.FormsIO.Converter.LoadDataModel.Load(contact.Data, formDocument);
            }
            return formDocument;
        }

        #endregion
    }
}
