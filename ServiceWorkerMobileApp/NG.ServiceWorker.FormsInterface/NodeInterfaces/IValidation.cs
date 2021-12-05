using System;

namespace NG.ServiceWorker.SwForms
{
    public class ValidationResult
    {
        /// <summary>Property for whether a form is valid.</summary>
        public bool IsFailed { get; private set; }

        /// <summary>The fail message to show in a prompt.</summary>
        public string FailMessage { get; private set; }
        /// <summary>The fail message to show against a field.</summary>
        public string FieldMessage { get; private set; }

        /// <summary>Nullable reference to the field that had the validation issue.</summary>
        public IFormField FailedField { get; private set; }

        /// <summary>Static singleton for a pass.</summary>
        public static ValidationResult Passed { get; private set; } = new ValidationResult { IsFailed = false };

        /// <summary>Creates a failed validation.</summary>
        public static ValidationResult CreateFail(string failMessage, string fieldMessage, IFormField failedField = null)
        {
            return new ValidationResult
            {
                IsFailed = true,
                FailMessage = failMessage,
                FieldMessage = fieldMessage,
                FailedField = failedField
            };
        }

        /// <summary>Private constructor to force static construction.</summary>
        private ValidationResult()
        {
            //
        }
    }

    [Flags]
    public enum ValidationFlags { None = 0x00, Mandatory = 0x01 }

    public interface IValidation
    {
        /// <summary>Getter for the validation flags.</summary>
        ValidationFlags ValidationFlags { get; }

        /// <summary>Getter for the validation result without making any changes.</summary>
        ValidationResult GetValidationResult();

        /// <summary>Performs the validation.</summary>
        ValidationResult Validate();
    }
}
