using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO
{
    public class FormsIoField : SwForms.IFormField
    {
        #region Base

        /// <summary>The form design.</summary>
        public ApiModel.FormIOModel.FormDesignComponent FieldDesign { get; private set; }

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
                            this.InputType = SwForms.FormFieldInputType.SegueMultiSelection;
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

        #endregion
    }
}
