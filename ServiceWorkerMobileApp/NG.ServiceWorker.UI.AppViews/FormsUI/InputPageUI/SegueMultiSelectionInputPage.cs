using System;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.InputPageUI
{
    public class SegueMultiSelectionInputPage : ContentPage
    {
        public SegueMultiSelectionInputPage(SegueMultiSelectionInputPageViewModel viewModel)
        {
            // Set title
            this.Title = viewModel.FormField.Label;

            // Create list view model
            Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel<ListUI.ListViewModel>(viewModel);
        }
    }
}
