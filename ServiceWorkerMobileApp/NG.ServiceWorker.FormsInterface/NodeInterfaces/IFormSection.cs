using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.SwForms
{
    public interface IFormSection
    {
        /// <summary>Getter for the section title.</summary>
        string SectionTitle { get; }

        /// <summary>The fields.</summary>
        IEnumerable<IFormField> Fields { get; }
    }
}
