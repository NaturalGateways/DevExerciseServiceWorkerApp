using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO
{
    public class FormsIoField : SwForms.IFormField, SwForms.IValidation, Rules.IRuleEffect
    {
        #region Base

        /// <summary>The form design.</summary>
        public ApiModel.FormIOModel.FormDesignComponent FieldDesign { get; private set; }

        /// <summary>The mandatory validation.</summary>
        public SwForms.Validation.MandatoryValidation MandatoryValidation { get; private set; }

        /// <summary>The list of cause rules.</summary>
        public List<SwForms.IRule> CauseRuleList = null;

        /// <summary>Construtor.</summary>
        public FormsIoField(ApiModel.FormIOModel.FormDesignComponent fieldDesign)
        {
            this.FieldDesign = fieldDesign;

            // Work out UI type
            if (this.FieldDesign.disabled)
            {
                this.InputType = SwForms.FormFieldInputType.ReadOnly;
            }
            else
            {
                switch (this.FieldDesign.type)
                {
                    case "datetime":
                        this.InputType = SwForms.FormFieldInputType.DateTime;
                        break;
                    case "select":
                        if (fieldDesign.data == null || fieldDesign.data.values == null)
                        {
                            this.InputType = SwForms.FormFieldInputType.ReadOnly;
                        }
                        else
                        {
                            this.InputType = fieldDesign.multiple ? SwForms.FormFieldInputType.SegueMultiSelection : SwForms.FormFieldInputType.SegueSingleSelection;
                            this.SelectList = new SelectLists.FormValuesSelectList(fieldDesign.data.values);
                        }
                        break;
                    case "radio":
                        if (fieldDesign.values == null)
                        {
                            this.InputType = SwForms.FormFieldInputType.ReadOnly;
                        }
                        else
                        {
                            this.SelectList = new SelectLists.FormValuesSelectList(fieldDesign.values);
                            if (fieldDesign.attributes?.ContainsKey("MobileUI") ?? false && fieldDesign.attributes["MobileUI"] == "Buttons")
                            {
                                this.InputType = SwForms.FormFieldInputType.ToggleButtonSingleSelection;
                            }
                            else
                            {
                                this.InputType = SwForms.FormFieldInputType.SegueSingleSelection;
                            }
                        }
                        break;
                }
            }

            // Check validation
            if (fieldDesign.validate?.required ?? false)
            {
                this.MandatoryValidation = new SwForms.Validation.MandatoryValidation(this);
            }
        }

        #endregion

        #region SwForms.IFormField implementation

        /// <summary>Getter for the input type of the field.</summary>
        public SwForms.FormFieldInputType InputType { get; private set; } = SwForms.FormFieldInputType.Text;

        /// <summary>Getter for the label.</summary>
        public string Label { get { return this.FieldDesign.label; } }

        /// <summary>Getter for the answer model.</summary>
        public SwForms.AnswerModel AnswerModel { get; set; } = new SwForms.AnswerModel();

        /// <summary>The select list if this is a selectable type.</summary>
        public SwForms.ISelectList SelectList { get; set; }

        /// <summary>The validation of the field.</summary>
        public SwForms.IValidation Validation { get { return this; } }

        /// <summary>The rule causes.</summary>
        public IEnumerable<SwForms.IRule> RuleCauses { get { return CauseRuleList; } }

        #endregion

        #region SwForms.IValidation implementation

        /// <summary>Getter for the validation flags.</summary>
        public SwForms.ValidationFlags ValidationFlags
        {
            get
            {
                SwForms.ValidationFlags totalFlags = SwForms.ValidationFlags.None;
                SwForms.Validation.MandatoryValidation mandatory = this.MandatoryValidation;
                if (mandatory != null)
                {
                    totalFlags = totalFlags | SwForms.ValidationFlags.Mandatory;
                }
                return totalFlags;
            }
        }

        /// <summary>Getter for the validation result without making any changes.</summary>
        public SwForms.ValidationResult GetValidationResult()
        {
            // Check mandatory validation
            SwForms.ValidationResult mandatory = this.MandatoryValidation?.GetValidationResult();
            if (mandatory?.IsFailed ?? false)
            {
                return mandatory;
            }
            // Otherwise everything passed
            return SwForms.ValidationResult.Passed;
        }

        /// <summary>Performs the validation.</summary>
        public SwForms.ValidationResult Validate()
        {
            SwForms.ValidationResult result = GetValidationResult();
            this.AnswerModel.Validation = result;
            this.AnswerModel.OnDataChanged();
            return result;
        }

        #endregion

        #region Rules.IRuleEffect implementation

        /// <summary>Proprty for whether the rule target is visible.</summary>
        public bool RuleEffectIsVisible { get; set; }

        /// <summary>Proprty for the effect model.</summary>
        public UI.Model EffectModel { get { return this.AnswerModel; } }

        #endregion
    }
}
