using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.SwForms
{
    public enum SelectListSelectType
    {
        Single,
        Multiple
    }

    public interface ISelectList
    {
        /// <summary>The select type.</summary>
        SelectListSelectType SelectType { get; }

        /// <summary>The lazy-loaded answers if this is a single level select list.</summary>
        ISelectListLevel FlatLevel { get; }
    }

    public interface ISelectListLevel
    {
        /// <summary>The selectable answers.</summary>
        IEnumerable<IAnswer> SelectableAnswers { get; }
    }
}
