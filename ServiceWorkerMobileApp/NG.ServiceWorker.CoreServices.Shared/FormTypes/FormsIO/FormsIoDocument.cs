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
