using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO
{
    public class FormsIoDocument : SwForms.IFormDocument, SwForms.IValidation
    {
        #region Base

        /// <summary>The form design.</summary>
        public ApiModel.FormIOModel.FormDesign FormDesign { get; private set; }

        /// <summary>The list of sections.</summary>
        public List<FormsIoSection> SectionList { get; private set; } = new List<FormsIoSection>();

        /// <summary>The list of all fields by their key.</summary>
        public Dictionary<string, FormsIoField> FieldsByKey { get; private set; } = new Dictionary<string, FormsIoField>();

        /// <summary>Construtor.</summary>
        public FormsIoDocument(ApiModel.FormIOModel.FormDesign formDesign)
        {
            this.FormDesign = formDesign;
        }

        #endregion

        #region SwForms.IFormDocument implementation

        /// <summary>The sections.</summary>
        public IEnumerable<SwForms.IFormSection> Sections { get { return this.SectionList; } }

        /// <summary>The validation of the document.</summary>
        public SwForms.IValidation Validation { get { return this; } }

        #endregion

        #region SwForms.IValidation implementation

        /// <summary>Getter for the validation flags.</summary>
        public SwForms.ValidationFlags ValidationFlags
        {
            get
            {
                SwForms.ValidationFlags totalFlags = SwForms.ValidationFlags.None;
                foreach (FormsIoField field in this.FieldsByKey.Values)
                {
                    totalFlags = totalFlags | field.ValidationFlags;
                }
                return totalFlags;
            }
        }

        /// <summary>Getter for the validation result without making any changes.</summary>
        public SwForms.ValidationResult GetValidationResult()
        {
            foreach (FormsIoField field in this.FieldsByKey.Values)
            {
                SwForms.ValidationResult fieldResult = field.GetValidationResult();
                if (fieldResult.IsFailed)
                {
                    return fieldResult;
                }
            }
            return SwForms.ValidationResult.Passed;
        }

        /// <summary>Performs the validation.</summary>
        public SwForms.ValidationResult Validate()
        {
            SwForms.ValidationResult failedResult = SwForms.ValidationResult.Passed;
            foreach (FormsIoField field in this.FieldsByKey.Values)
            {
                SwForms.ValidationResult fieldResult = field.Validate();
                if (failedResult.IsFailed == false && fieldResult.IsFailed)
                {
                    failedResult = fieldResult;
                }
            }
            return failedResult;
        }

        #endregion
    }
}
