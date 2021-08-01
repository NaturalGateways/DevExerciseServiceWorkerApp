using System;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsTextFieldViewModel : FormsFieldViewModel
    {
        /// <summary>Constructor.</summary>
        public FormsTextFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            //
        }

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>The value text.</summary>
        public string ValueText
        {
            get { return this.FormField.AnswerModel.Answer.DisplayValue; }
            set
            {
                this.FormField.AnswerModel.Answer = new SwForms.Answers.StringAnswer(value);
                this.FormField.AnswerModel.OnDataChanged();
            }
        }
    }
}
