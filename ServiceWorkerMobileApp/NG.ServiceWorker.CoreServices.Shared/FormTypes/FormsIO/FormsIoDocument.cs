using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO
{
    public class FormsIoDocument : SwForms.IFormDocument
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

        #endregion
    }
}
