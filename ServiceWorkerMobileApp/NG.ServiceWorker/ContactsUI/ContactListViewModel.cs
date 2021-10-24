using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public class ContactListViewModel
    {
        public ImageSource AddImageSource { get { return Services.SvgService.GetPngFile("icon_add", 30, null).AsImageSource; } }

        public ICommand AddCommand { get; set; }

        public ObservableCollection<ContactListItemViewModel> ItemViewModels { get; private set; } = new ObservableCollection<ContactListItemViewModel>();
    }
}
