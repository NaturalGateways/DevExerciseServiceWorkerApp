using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI
{
    public partial class FormsSectionView : ContentView
    {
        public FormsSectionView(FormsSectionViewModel viewModel)
        {
            InitializeComponent();

            if (viewModel.FieldViewModelArray != null)
            {
                foreach (FormsFieldViewModel fieldViewModel in viewModel.FieldViewModelArray)
                {
                    View fieldView = Services.UserInterfaceViewFactoryService.CreateViewFromAppViewModel(fieldViewModel);
                    this.FieldStack.Children.Add(fieldView);
                }
            }
        }
    }
}
