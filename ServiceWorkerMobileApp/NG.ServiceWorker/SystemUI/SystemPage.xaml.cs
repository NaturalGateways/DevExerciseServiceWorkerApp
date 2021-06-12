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
        public SystemPage()
        {
            InitializeComponent();

            // Create list
            UI.ListUI.ListViewModel listViewModel = new UI.ListUI.ListViewModel();
            listViewModel.SectionList.Add(new UI.ListUI.ListSectionViewModel { Title = "General" });
            listViewModel.SectionList[0].Add(UI.ListUI.ListItemViewModel.CreateCommand("About", GotoAbout));
            this.Content = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(listViewModel);
        }

        public async Task GotoAbout(View view)
        {
            await view.Navigation.PushAsync(new SystemAboutPage());
        }
    }
}
