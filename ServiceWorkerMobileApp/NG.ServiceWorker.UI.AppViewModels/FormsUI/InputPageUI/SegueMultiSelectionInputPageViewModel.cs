using System;
using System.Threading.Tasks;

namespace NG.ServiceWorker.UI.FormsUI.InputPageUI
{
    public class SegueMultiSelectionInputPageViewModel : ListUI.ListViewModel
    {
        /// <summary>The form field.</summary>
        public SwForms.IFormField FormField { get; private set; }

        /// <summary>Constructor.</summary>
        public SegueMultiSelectionInputPageViewModel(SwForms.IFormField formField)
        {
            this.FormField = formField;

            // Create single section
            ListUI.ListSectionViewModel sectionViewModel = new ListUI.ListSectionViewModel();

            // Work out list of items
            foreach (SwForms.IAnswer answer in formField.SelectList.FlatLevel.SelectableAnswers)
            {
                sectionViewModel.Add(new SegueMultiSelectionInputItemViewModel(formField, answer));
            }

            // Add section
            this.SectionList.Add(sectionViewModel);
        }
    }
}
