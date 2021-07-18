using System;

namespace NG.ServiceWorker.UI.FormsUI
{
    public class FormsFieldViewModel : AppViewModel
    {
        /// <summary>The form field.</summary>
        public SwForms.IFormField FormField { get; private set; }

        /// <summary>Constructor.</summary>
        public FormsFieldViewModel(SwForms.IFormField formField)
        {
            this.FormField = formField;
        }

        /// <summary>Creates the ccorrect view model for a field.</summary>
        public static FormsFieldViewModel FromField(SwForms.IFormField formField)
        {
            switch (formField.InputType)
            {
                case SwForms.FormFieldInputType.DateTime:
                    return new FieldsUI.FormsDateTimeFieldViewModel(formField);
                case SwForms.FormFieldInputType.SegueMultiSelection:
                case SwForms.FormFieldInputType.SegueSingleSelection:
                    return new FieldsUI.FormsSegueSelectionFieldViewModel(formField);
                case SwForms.FormFieldInputType.ReadOnly:
                    return new FieldsUI.FormsReadOnlyFieldViewModel(formField);
                case SwForms.FormFieldInputType.ToggleButtonSingleSelection:
                    return new FieldsUI.FormsToggleButtonSelectionFieldViewModel(formField);
                default:
                    return new FieldsUI.FormsTextFieldViewModel(formField);
            }
        }
    }
}
