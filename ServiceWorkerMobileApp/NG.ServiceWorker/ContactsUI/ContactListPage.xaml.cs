using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public partial class ContactListPage : ContentPage, UI.IModelListener
    {
        #region Base

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
                    UIModel.ContactListModel contactListModel = Services.UserInterfaceActiveDataService.ContactList;

                    // Thread lock
                    lock (contactListModel)
                    {
                        // Create items
                        if (contactListModel.ContactList?.Any() ?? false)
                        {
                            foreach (UIModel.ContactListContactModel contact in contactListModel.ContactList)
                            {
                                m_viewModel.ItemViewModels.Add(new ContactListItemViewModel(contact)
                                {
                                    XamarinView = this
                                });
                            }
                        }

                        // Listen for further changes
                        contactListModel.AddListener(this);
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

        #endregion

        #region UI.IModelListener

        /// <summary>Called when the data has changed.</summary>
        public void OnDataChanged(UI.Model model)
        {
            // Run on backgroud thread
            Services.ThreadService.RunActionOnBackgroundThread("UpdateContactList", () =>
            {
                try
                {
                    // Get data
                    UIModel.ContactListModel contactListModel = Services.UserInterfaceActiveDataService.ContactList;

                    // Thread lock
                    lock (contactListModel)
                    {
                        // Create items
                        Dictionary<string, ContactListItemViewModel> oldItemsById = m_viewModel.ItemViewModels.ToDictionary(x => x.ContactSummary.ContactId);
                        Dictionary<string, UIModel.ContactListContactModel> newSummariesById = contactListModel.ContactList.ToDictionary(x => x.ContactSummary.ContactId);
                        foreach (ContactListItemViewModel doomedItem in oldItemsById.Where(x => newSummariesById.ContainsKey(x.Key) == false).Select(x => x.Value))
                        {
                            m_viewModel.ItemViewModels.Remove(doomedItem);
                        }
                        foreach (UIModel.ContactListContactModel newModel in newSummariesById.Where(x => oldItemsById.ContainsKey(x.Key) == false).Select(x => x.Value))
                        {
                            m_viewModel.ItemViewModels.Insert(0, new ContactListItemViewModel(newModel)
                            {
                                XamarinView = this
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Services.LogService.CreateLogger("ContactsUpdate").Error("Error updating contacts.", ex);
                }
            });
        }

        #endregion
    }
}
