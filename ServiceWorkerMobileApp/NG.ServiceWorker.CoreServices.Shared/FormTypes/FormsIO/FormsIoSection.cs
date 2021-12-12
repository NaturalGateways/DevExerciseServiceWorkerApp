using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO
{
    public class FormsIoSection : SwForms.IFormSection, Rules.IRuleEffect
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

        /// <summary>the section model that UI can use to listen for changes..</summary>
        public UI.Model SectionModel { get; private set; } = new UI.Model();

        /// <summary>Getter for the section title.</summary>
        public string SectionTitle { get { return this.SectionDesign.title; } }

        /// <summary>Getter for whether the section should be visible.</summary>
        public bool SectionIsVisible { get; set; } = true;

        /// <summary>The fields.</summary>
        public IEnumerable<SwForms.IFormField> Fields { get { return this.FieldList; } }

        #endregion

        #region Rules.IRuleEffect implementation

        /// <summary>Proprty for whether the rule target is visible.</summary>
        public bool RuleEffectIsVisible { get { return this.SectionIsVisible; } set { this.SectionIsVisible = value; } }

        /// <summary>Proprty for the effect model.</summary>
        public UI.Model EffectModel { get { return this.SectionModel; } }

        #endregion
    }
}
