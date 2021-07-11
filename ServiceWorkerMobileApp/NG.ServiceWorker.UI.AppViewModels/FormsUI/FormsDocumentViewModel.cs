using System;
using System.Linq;

namespace NG.ServiceWorker.UI.FormsUI
{
    public class FormsDocumentViewModel : ViewModel
    {
        public SwForms.IFormDocument Document { get; set; }

        public FormsSectionViewModel[] SectionViewModelArray { get; private set; }

        /// <summary>Constructor.</summary>
        public FormsDocumentViewModel(SwForms.IFormDocument document)
        {
            this.Document = document;

            if (document.Sections != null)
            {
                this.SectionViewModelArray = document.Sections.Select(x => new FormsSectionViewModel(x)).ToArray();
            }
        }
    }
}
