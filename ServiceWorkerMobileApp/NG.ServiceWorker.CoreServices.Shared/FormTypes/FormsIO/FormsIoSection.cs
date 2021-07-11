using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO
{
    public class FormsIoSection : SwForms.IFormSection
    {
        #region Base

        /// <summary>The form design.</summary>
        public ApiModel.FormIOModel.FormDesignComponent SectionDesign { get; private set; }

        /// <summary>The list of fields.</summary>
        public List<FormsIoField> FieldList { get; private set; } = new List<FormsIoField>();

        /// <summary>Construtor.</summary>
        public FormsIoSection(ApiModel.FormIOModel.FormDesignComponent sectionDesign)
        {
            this.SectionDesign = sectionDesign;
        }

        #endregion

        #region SwForms.IFormSection implementation

        /// <summary>Getter for the section title.</summary>
        public string SectionTitle { get { return this.SectionDesign.title; } }

        /// <summary>The fields.</summary>
        public IEnumerable<SwForms.IFormField> Fields { get { return this.FieldList; } }

        #endregion
    }
}
