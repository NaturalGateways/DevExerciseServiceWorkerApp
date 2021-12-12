using System;
using System.Linq;

namespace NG.ServiceWorker.UI.FormsUI
{
    public class FormsSectionViewModel : ViewModel, IModelListener
    {
        #region Base

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

            section.SectionModel.AddListener(this);
        }

        #endregion

        #region IModelListener implementation

        /// <summary>Called when the data has changed.</summary>
        public void OnDataChanged(Model model)
        {
            OnPropertyChanged("IsVisible");
        }

        #endregion

        #region XAML Internal Bindings

        public string TitleText { get { return this.Section.SectionTitle; } }

        public bool IsVisible { get { return this.Section.SectionIsVisible; } }

        #endregion
    }
}
