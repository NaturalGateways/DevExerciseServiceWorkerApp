using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI
{
    public partial class FormsDocumentView : ContentView
    {
        public FormsDocumentView(FormsDocumentViewModel viewModel)
        {
            InitializeComponent();

            if (viewModel.SectionViewModelArray != null)
            {
                foreach (FormsSectionViewModel sectionViewModel in viewModel.SectionViewModelArray)
                {
                    View sectionView = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(sectionViewModel);
                    this.SectionStack.Children.Add(sectionView);
                }
            }
        }
    }
}
