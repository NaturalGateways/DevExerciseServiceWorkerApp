using System;

namespace NG.ServiceWorker.SwForms.Validation
{
    public class MandatoryValidation : IValidation
    {
        #region Base

        /// <summary>The form field being validated.</summary>
        public IFormField FormField { get; private set; }

        /// <summary>Constructor.</summary>
        public MandatoryValidation(IFormField formField)
        {
            this.FormField = formField;
        }

        #endregion

        #region IValidation implementation

        /// <summary>Getter for the validation flags.</summary>
        public ValidationFlags ValidationFlags { get { return ValidationFlags.Mandatory; } }

        /// <summary>Getter for the validation result without making any changes.</summary>
        public ValidationResult GetValidationResult()
        {
            if (string.IsNullOrEmpty(this.FormField.AnswerModel.Answer.CodeValue))
            {
                return ValidationResult.CreateFail($"Field '{this.FormField.Label}' is mandatory.", "Field is mandatory.", this.FormField);
            }
            return ValidationResult.Passed;
        }

        /// <summary>Performs the validation.</summary>
        public ValidationResult Validate()
        {
            return GetValidationResult();
        }

        #endregion
    }
}
