using System;
using System.Linq;

namespace NG.ServiceWorker.UI.FormsUI
{
    public class FormsSectionViewModel : ViewModel
    {
        public SwForms.IFormSection Section { get; set; }

        public FormsFieldViewModel[] FieldViewModelArray { get; private set; }

        /// <summary>Constructor.</summary>
        public FormsSectionViewModel(SwForms.IFormSection section)
        {
            this.Section = section;

            if (section.Fields != null)
            {
                this.FieldViewModelArray = section.Fields.Select(x => FormsFieldViewModel.FromField(x)).ToArray();
            }
        }

        public string TitleText { get { return this.Section.SectionTitle; } }
    }
}
