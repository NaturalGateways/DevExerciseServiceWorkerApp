using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsToggleButtonSelectionFieldViewModel : FormsFieldViewModel
    {
        #region Base

        /// <summary>Constructor.</summary>
        public FormsToggleButtonSelectionFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            //
        }

        #endregion

        #region Internal XAML properties

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        #endregion
    }
}
