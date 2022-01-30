using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.SwForms
{
    public enum FormFieldInputType
    {
        Address,
        DateTime,
        ReadOnly,
        SegueMultiSelection,
        SegueSingleSelection,
        ToggleButtonSingleSelection,
        Text
    }

    public interface IFormField
    {
        /// <summary>Getter for the input type of the field.</summary>
        FormFieldInputType InputType { get; }

        /// <summary>Getter for the label.</summary>
        string Label { get; }

        /// <summary>Getter for the answer model.</summary>
        AnswerModel AnswerModel { get; }

        /// <summary>The select list if this is a selectable type.</summary>
        ISelectList SelectList { get; }

        /// <summary>The validation of the field.</summary>
        IValidation Validation { get; }

        /// <summary>The rule causes.</summary>
        IEnumerable<IRule> RuleCauses { get; }
    }
}
