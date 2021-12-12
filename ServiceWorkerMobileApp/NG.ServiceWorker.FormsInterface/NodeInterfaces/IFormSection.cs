using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.SwForms
{
    public interface IFormSection
    {
        /// <summary>the section model that UI can use to listen for changes..</summary>
        UI.Model SectionModel { get; }

        /// <summary>Getter for the section title.</summary>
        string SectionTitle { get; }

        /// <summary>Getter for whether the section should be visible.</summary>
        bool SectionIsVisible { get; }

        /// <summary>The fields.</summary>
        IEnumerable<IFormField> Fields { get; }
    }
}
