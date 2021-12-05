using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.SwForms
{
    public interface IFormDocument
    {
        /// <summary>The sections.</summary>
        IEnumerable<IFormSection> Sections { get; }

        /// <summary>The validation of the document.</summary>
        IValidation Validation { get; }
    }
}
