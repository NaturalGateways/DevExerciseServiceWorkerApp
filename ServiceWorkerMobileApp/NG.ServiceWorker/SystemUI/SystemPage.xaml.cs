using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NG.ServiceWorker.SystemUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemPage : ContentPage
    {
        public ObservableCollection<AppViews.ListUI.ListItemViewModel> Items { get; set; }

        public SystemPage()
        {
            InitializeComponent();

            // Create list
            ListViewModels.ListViewModel listViewModel = new ListViewModels.ListViewModel();
            listViewModel.SectionList.Add(new ListViewModels.ListSectionViewModel { Title = "General" });
            listViewModel.SectionList[0].Add(ListViewModels.ListItemViewModel.CreateCommand("About", GotoAbout));
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GotoAbout(View view)
        {
            await view.Navigation.PushAsync(new SystemAboutPage());
        }
    }
}
