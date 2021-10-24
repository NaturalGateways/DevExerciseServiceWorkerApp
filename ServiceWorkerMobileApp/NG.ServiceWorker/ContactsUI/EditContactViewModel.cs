using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public class EditContactViewModel : UI.ViewModel
    {
        #region Base

        public Page XamarinPage { get; set; }

        public UIModel.ContactListContactModel ContactItem { get; private set; }

        public DataModel.ContactSummary Summary { get; private set; }

        public SwForms.IFormDocument FormsDocument { get; private set; }

        public EditContactViewModel(UIModel.ContactListContactModel contactItem)
        {
            this.ContactItem = contactItem;
            this.Summary = contactItem.ContactSummary;

            // Load the form
            this.FormsDocument = Services.FormsService.CreateContactEditForm(contactItem.ContactSummary.ContactId);
            UI.FormsUI.FormsDocumentViewModel formViewModel = new UI.FormsUI.FormsDocumentViewModel(this.FormsDocument);
            this.FormsView = Services.UserInterfaceViewFactoryService.CreateViewFromViewModel(formViewModel);
        }

        #endregion

        #region Internal Binding Properties

        public string Title { get { return "Edit Contact"; } }

        public View FormsView { get; private set; }

        public ImageSource SaveImageSource { get { return Services.SvgService.GetPngFile("icon_save", 30, null).AsImageSource; } }

        public ICommand SaveCommand { get; set; }

        #endregion
    }
}
