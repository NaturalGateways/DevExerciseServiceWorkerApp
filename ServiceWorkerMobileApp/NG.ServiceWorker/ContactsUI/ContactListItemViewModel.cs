using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public class ContactListItemViewModel : UI.ViewModel, UI.IModelListener
    {
        #region Base

        public Page XamarinView { get; set; }

        public UIModel.ContactListContactModel Contact { get; private set; }

        public DataModel.ContactSummary ContactSummary { get { return this.Contact.ContactSummary; } }

        public ContactListItemViewModel(UIModel.ContactListContactModel contact)
        {
            this.Contact = contact;
            this.Contact.AddListener(this);
        }

        #endregion

        #region UI.IModelListener implementation

        /// <summary>Called when the data has changed.</summary>
        public void OnDataChanged(UI.Model model)
        {
            OnPropertyChanged("DisplayName");
            OnPropertyChanged("BusinessName");
            OnPropertyChanged("Address");
        }

        #endregion

        #region Internal Binding Properties

        public string DisplayName { get { return this.ContactSummary.FullName; } }

        public string BusinessName { get { return string.IsNullOrEmpty(this.ContactSummary.BusinessName) ? "Private" : this.ContactSummary.BusinessName; } }

        public string Address { get { return string.IsNullOrEmpty(this.ContactSummary.Address) ? "No address" : this.ContactSummary.Address; } }

        #endregion

        #region Internal Binding Commands

        public ICommand TappedCommand => new Command(OnItemTapped);

        private void OnItemTapped()
        {
            EditContactViewModel editViewModel = new EditContactViewModel(this.Contact);
            Page editPage = Services.UserInterfaceViewFactoryService.CreatePageFromViewModel<EditContactViewModel>(editViewModel);
            this.XamarinView.Navigation.PushAsync(editPage);
        }

        #endregion
    }
}
