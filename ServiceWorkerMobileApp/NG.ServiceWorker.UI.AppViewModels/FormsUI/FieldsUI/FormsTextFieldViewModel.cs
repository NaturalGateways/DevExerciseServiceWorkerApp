using System;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsTextFieldViewModel : FormsFieldViewModel, IModelListener
    {
        #region Base

        /// <summary>Constructor.</summary>
        public FormsTextFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            // Listen for changes
            formField.AnswerModel.AddListener(this);
        }

        #endregion

        #region IModelListener implementation

        /// <summary>Called when the data has changed.</summary>
        public void OnDataChanged(Model model)
        {
            OnPropertyChanged("ValueText");
            OnPropertyChanged("ValidationIsShowing");
            OnPropertyChanged("ValidationText");
        }

        #endregion

        #region Internal XAML properties

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>Bound property.</summary>
        public bool MandatoryIsShowing { get { if (this.FormField.Validation == null) { return false; } return this.FormField.Validation.ValidationFlags.HasFlag(SwForms.ValidationFlags.Mandatory); } }

        /// <summary>The value text.</summary>
        public string ValueText
        {
            get { return this.FormField.AnswerModel.Answer.DisplayValue; }
            set
            {
                SwForms.FormsHelper.SetAnswer(this.FormField, new SwForms.Answers.StringAnswer(value));
            }
        }

        /// <summary>Bound property.</summary>
        public bool ValidationIsShowing { get { return this.FormField.AnswerModel.Validation.IsFailed; } }
        /// <summary>Bound property.</summary>
        public string ValidationText { get { return this.FormField.AnswerModel.Validation.FieldMessage ?? string.Empty; } }

        #endregion
    }
}
