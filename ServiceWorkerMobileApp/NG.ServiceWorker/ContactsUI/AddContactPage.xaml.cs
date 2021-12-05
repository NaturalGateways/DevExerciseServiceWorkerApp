using System;
using System.Collections.Generic;
using System.Linq;

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
            // Check validation
            SwForms.ValidationResult validation = m_viewModel.FormsDocument.Validation?.Validate();
            if (validation?.IsFailed ?? false)
            {
                Services.UIDialogService.ShowMessageAsync("Create Contact Error", validation.FailMessage).Wait();
                return;
            }

            // Save
            object formInstanceData = Services.FormsService.ConvertFormToFormInstance(m_viewModel.FormsDocument);
            IJsonObject formInstanceJson = Services.JsonService.JsonObjectFromAnonObject(formInstanceData);

            // Construct the contact full name
            string[] fullNameComponents = new string[]
            {
                formInstanceJson.GetDictionaryObject("title").AsString,
                formInstanceJson.GetDictionaryObject("givenName").AsString,
                formInstanceJson.GetDictionaryObject("familyName").AsString
            };

            // Create contact data
            DataModel.Contact contact = new DataModel.Contact
            {
                ContactId = Guid.NewGuid().ToString(),
                FullName = string.Join(" ", fullNameComponents.Where(x => string.IsNullOrEmpty(x) == false)),
                BusinessName = formInstanceJson.GetDictionaryObject("businessName").AsString,
                Address = formInstanceJson.GetDictionaryObject("address").AsString,
                Data = formInstanceData
            };
            DataModel.ContactSummary contactSummary = new DataModel.ContactSummary
            {
                ContactId = contact.ContactId,
                FullName = contact.FullName,
                BusinessName = contact.BusinessName,
                Address = contact.Address
            };
            // Save
            Services.MainDataService.SetEntity(EntityType.Contact, contact.ContactId, new MainDataEntityDataItem[]
            {
                MainDataEntityDataItem.Create<DataModel.Contact>(null, contact),
                MainDataEntityDataItem.Create<DataModel.ContactSummary>(null, contactSummary)
            });

            // Add to UI
            UIModel.ContactListModel contactListModel = Services.UserInterfaceActiveDataService.ContactList;
            lock (contactListModel)
            {
                contactListModel.ContactList.Add(new UIModel.ContactListContactModel(contactSummary));
                contactListModel.OnDataChanged();
            }

            // Segue back
            this.Navigation.PopAsync();
        }
    }
}
