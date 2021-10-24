using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public partial class EditContactPage : ContentPage
    {
        private EditContactViewModel m_viewModel = null;

        public EditContactPage(EditContactViewModel viewModel)
        {
            InitializeComponent();

            m_viewModel = viewModel;
            m_viewModel.XamarinPage = this;
            m_viewModel.SaveCommand = new Command(OnSaveContact);
        }

        private void OnSaveContact()
        {
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
                ContactId = m_viewModel.Summary.ContactId,
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

            // Update contact UI
            m_viewModel.ContactItem.ContactSummary = contactSummary;
            m_viewModel.ContactItem.OnDataChanged();

            // Segue back
            this.Navigation.PopAsync();
        }
    }
}
