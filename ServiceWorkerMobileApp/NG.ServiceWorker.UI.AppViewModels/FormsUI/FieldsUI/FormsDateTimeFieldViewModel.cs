using System;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsDateTimeFieldViewModel : FormsFieldViewModel
    {
        /// <summary>Constructor.</summary>
        public FormsDateTimeFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            //
        }

        /// <summary>The current date and time.</summary>
        public DateTime CurrentDateTime
        {
            get { return DateTime.Now; }
        }

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>The date component of the value.</summary>
        public DateTime DateValue
        {
            get { return this.CurrentDateTime.Date; }
        }

        /// <summary>The time component of the value.</summary>
        public TimeSpan TimeValue
        {
            get { return this.CurrentDateTime.TimeOfDay; }
        }
    }
}
