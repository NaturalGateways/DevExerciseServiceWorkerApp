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

            Items = new ObservableCollection<AppViews.ListUI.ListItemViewModel>
            {
                new AppViews.ListUI.ListItemViewModel { MainText = "About" }
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await this.Navigation.PushAsync(new SystemAboutPage());

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
