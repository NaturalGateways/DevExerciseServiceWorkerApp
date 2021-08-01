using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultFormsServices : IFormsService
    {
        #region IFormsService implementation

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

        #endregion
    }
}
