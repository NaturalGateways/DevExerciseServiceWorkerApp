using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public partial class ContactListPage : ContentPage
    {
        private ContactListViewModel m_viewModel = null;

        public ContactListPage()
        {
            InitializeComponent();

            // Create view model
            m_viewModel = new ContactListViewModel
            {
                AddCommand = new Command(OnAddContact)
            };
            this.BindingContext = m_viewModel;

            // Run on backgroud thread
            Services.ThreadService.RunActionOnBackgroundThread("LoadContactsOnStartup", () =>
            {
                try
                {
                    // Get data
                    IEnumerable<DataModel.ContactSummary> contactSummaries = Services.MainDataService.GetEntitys<DataModel.ContactSummary>(EntityType.Contact);

                    // Create items
                    if (contactSummaries?.Any() ?? false)
                    {
                        foreach (DataModel.ContactSummary contactSummary in contactSummaries)
                        {
                            m_viewModel.ItemViewModels.Add(new ContactListItemViewModel
                            {
                                ContactSummary = contactSummary
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Services.LogService.CreateLogger("ContactsLoad").Error("Error loading contacts.", ex);
                }
            });
        }

        private void OnAddContact()
        {
            // Load contact form
            DataModel.FormDesign createContactFormDesign = Services.MainDataService.GetRefListItem<DataModel.FormDesign>(null, "contact");

            // Create forms view
            SwForms.IFormDocument formsDocument = Services.FormsService.CreateFormFromDesign(createContactFormDesign);
            UI.FormsUI.FormsDocumentViewModel formViewModel = new UI.FormsUI.FormsDocumentViewModel(formsDocument);
            View formsView = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(formViewModel);

            // Create page
            AddContactViewModel pageViewModel = new AddContactViewModel
            {
                FormDesign = createContactFormDesign,
                FormsDocument = formsDocument,
                FormsView = formsView
            };
            Page page = Services.UserInterfaceViewFactoryService.CreatePageFromViewModel<AddContactViewModel>(pageViewModel);
            this.Navigation.PushAsync(page);
        }
    }
}
