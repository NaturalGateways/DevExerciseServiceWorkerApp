using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.DebugUI
{
    public partial class DebugPage : ContentPage
    {
        public DebugPage()
        {
            InitializeComponent();

            // Create list
            ListViewModels.ListViewModel listViewModel = new ListViewModels.ListViewModel();
            listViewModel.SectionList.Add(new ListViewModels.ListSectionViewModel { Title = "General" });
            listViewModel.SectionList[0].Add(ListViewModels.ListItemViewModel.CreateCommand("Filesystem", GotoFilesystem));
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GotoFilesystem(View view)
        {
            await view.Navigation.PushAsync(new FilesystemDebugUI.DebugFilesystemPage());
        }
    }
}
