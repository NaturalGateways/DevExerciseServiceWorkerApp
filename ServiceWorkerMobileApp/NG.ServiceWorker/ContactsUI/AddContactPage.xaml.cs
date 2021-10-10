using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public partial class AddContactPage : ContentPage
    {
        private AddContactViewModel m_viewModel = null;

        public AddContactPage(AddContactViewModel viewModel)
        {
            InitializeComponent();

            m_viewModel = viewModel;
            viewModel.SaveCommand = new Command(OnSaveContact);
        }

        private void OnSaveContact()
        {
            // Save
            object formInstanceData = Services.FormsService.ConvertFormToFormInstance(m_viewModel.FormsDocument);
            IJsonObject formInstanceJson = Services.JsonService.JsonObjectFromAnonObject(formInstanceData);

            // Create contact data
            DataModel.Contact contact = new DataModel.Contact
            {
                ContactId = Guid.NewGuid().ToString(),
                FullName = formInstanceJson.GetDictionaryObject("givenName").AsString,
                Address = formInstanceJson.GetDictionaryObject("address").AsString,
                Data = formInstanceData
            };
            DataModel.ContactSummary contactSummary = new DataModel.ContactSummary
            {
                ContactId = contact.ContactId,
                FullName = contact.FullName,
                Address = contact.Address
            };
            // Save
            Services.MainDataService.SetEntity(EntityType.Contact, contact.ContactId, new MainDataEntityDataItem[]
            {
                MainDataEntityDataItem.Create<DataModel.Contact>(null, contact),
                MainDataEntityDataItem.Create<DataModel.ContactSummary>(null, contactSummary)
            });

            // Segue back
            this.Navigation.PopAsync();
        }
    }
}
