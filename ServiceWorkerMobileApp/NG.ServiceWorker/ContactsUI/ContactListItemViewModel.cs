using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public class ContactListItemViewModel : UI.ViewModel
    {
        public DataModel.ContactSummary ContactSummary { get; set; }

        public string DisplayName { get { return this.ContactSummary.FullName; } }

        public string Address { get { return this.ContactSummary.Address; } }
    }
}
