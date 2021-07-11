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
        }

        #endregion

        #region SwForms.IFormField implementation

        /// <summary>Getter for the label.</summary>
        public string Label { get { return this.FieldDesign.label; } }

        #endregion
    }
}
