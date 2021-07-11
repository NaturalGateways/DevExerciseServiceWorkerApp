using System;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsReadOnlyFieldViewModel : FormsFieldViewModel
    {
        /// <summary>Constructor.</summary>
        public FormsReadOnlyFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            //
        }

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>The value text.</summary>
        public string ValueText { get { return this.FormField.AnswerModel.Answer.DisplayValue; } }
        /// <summary>The text to show when there is no value.</summary>
        public string NoValuePlaceholderText { get { return "No value"; } }

        /// <summary>Visibility property.</summary>
        public bool IsValueVisible { get { return string.IsNullOrEmpty(this.ValueText) == false; } }
        /// <summary>Visibility property.</summary>
        public bool IsNoValueVisible { get { return string.IsNullOrEmpty(this.ValueText); } }
    }
}
