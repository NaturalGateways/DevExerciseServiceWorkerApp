using System;
using System.Threading.Tasks;

namespace NG.ServiceWorker.UI.FormsUI.InputPageUI
{
    public class SegueSelectionInputPageViewModel : ListUI.ListViewModel
    {
        /// <summary>The form field.</summary>
        public SwForms.IFormField FormField { get; private set; }

        /// <summary>Constructor.</summary>
        public SegueSelectionInputPageViewModel(SwForms.IFormField formField)
        {
            this.FormField = formField;

            // Create single section
            ListUI.ListSectionViewModel sectionViewModel = new ListUI.ListSectionViewModel();

            // Work out list of items
            SwForms.IAnswer selectedAnswer = formField.AnswerModel.Answer;
            foreach (SwForms.IAnswer answer in formField.SelectList.FlatLevel.SelectableAnswers)
            {
                bool selected = object.ReferenceEquals(answer, selectedAnswer);
                ListUI.ListItemViewModel itemViewModel = ListUI.ListItemViewModel.CreateSelectableItem(answer.DisplayValue, selected, async (view) => { await OnItemClicked(view, answer); });
                sectionViewModel.Add(itemViewModel);
            }

            // Add section
            this.SectionList.Add(sectionViewModel);
        }

        /// <summary>Called when the item is clicked.</summary>
        private async Task OnItemClicked(Xamarin.Forms.View view, SwForms.IAnswer answer)
        {
            // Set answer
            SwForms.FormsHelper.SetAnswer(this.FormField, answer);

            // Go back
            await view.Navigation.PopAsync();
        }
    }
}
