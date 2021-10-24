using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.ContactsUI
{
    public class AddContactViewModel : UI.ViewModel
    {
        public DataModel.FormDesign FormDesign { get; set; }

        public SwForms.IFormDocument FormsDocument { get; set; }
        public View FormsView { get; set; }

        public ImageSource SaveImageSource { get { return Services.SvgService.GetPngFile("icon_save", 30, null).AsImageSource; } }

        public ICommand SaveCommand { get; set; }
    }
}
