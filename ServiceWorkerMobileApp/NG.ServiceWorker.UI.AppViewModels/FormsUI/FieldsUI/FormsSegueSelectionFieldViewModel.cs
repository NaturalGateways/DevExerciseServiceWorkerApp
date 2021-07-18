using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsSegueSelectionFieldViewModel : FormsFieldViewModel, IModelListener
    {
        #region Base

        /// <summary>Constructor.</summary>
        public FormsSegueSelectionFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            // Listen to changing answer
            formField.AnswerModel.AddListener(this);
        }

        /// <summary>Called when the user clicks the edit button.</summary>
        private async Task OnEditClicked()
        {
            // Get view for naviagtion
            View view = this.View as View;
            // Create page to segue to
            if (this.FormField.InputType == SwForms.FormFieldInputType.SegueMultiSelection)
            {
                InputPageUI.SegueMultiSelectionInputPageViewModel segueViewModel = new InputPageUI.SegueMultiSelectionInputPageViewModel(this.FormField);
                Page seguePage = Services.UserInterfaceViewFactoryService.CreatePageFromViewModel(segueViewModel);
                await view.Navigation.PushAsync(seguePage);
            }
            else
            {
                InputPageUI.SegueSelectionInputPageViewModel segueViewModel = new InputPageUI.SegueSelectionInputPageViewModel(this.FormField);
                Page seguePage = Services.UserInterfaceViewFactoryService.CreatePageFromViewModel(segueViewModel);
                await view.Navigation.PushAsync(seguePage);
            }
        }

        #endregion

        #region IModelListener implementation

        /// <summary>Called when the data has cchanged.</summary>
        public void OnDataChanged(Model model)
        {
            OnPropertyChanged("ValueText");
            OnPropertyChanged("IsValueVisible");
            OnPropertyChanged("IsNoValueVisible");
        }

        #endregion

        #region Internal XAML properties

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>The value text.</summary>
        public string ValueText { get { return this.FormField.AnswerModel.Answer.DisplayValue; } }
        /// <summary>The text to show when there is no value.</summary>
        public string NoValuePlaceholderText { get { return "No value"; } }

        /// <summary>Visibility property.</summary>
        public bool IsValueVisible { get { return string.IsNullOrEmpty(this.ValueText) == false; } }
        /// <summary>Visibility property.</summary>
        public bool IsNoValueVisible { get { return string.IsNullOrEmpty(this.ValueText); } }

        #endregion

        #region Internal XAML commands

        /// <summary>The edit command.</summary>
        public ICommand EditCommand => new Command(async () => { await OnEditClicked(); });

        #endregion
    }
}
