using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NG.ServiceWorker.AppViews.ListUI
{
    public partial class ListView : ContentView
    {
        public ListView()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            // Get view model
            ListViewModels.ListItemViewModel itemViewModel = e.Item as ListViewModels.ListItemViewModel;
            if (itemViewModel == null)
            {
                return;
            }

            // Invoke
            if (itemViewModel.ClickFuncAsync != null)
            {
                await itemViewModel.ClickFuncAsync.Invoke(this);
            }
        }
    }
}
