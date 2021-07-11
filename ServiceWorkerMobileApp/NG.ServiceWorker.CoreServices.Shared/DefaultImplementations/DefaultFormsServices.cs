using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultFormsServices : IFormsService
    {
        #region IFormsService implementation

        /// <summary>Creates a job form.</summary>
        public SwForms.IFormDocument CreateJobForm()
        {
            ApiModel.FormIOModel.FormDesign formDesign = Services.ApiService.GetJobFormDesign();
            SwForms.IFormDocument formDocument = FormTypes.FormsIO.Converter.ApiModel2SwForms.CreateDocument(formDesign);
            return formDocument;
        }

        #endregion
    }
}
