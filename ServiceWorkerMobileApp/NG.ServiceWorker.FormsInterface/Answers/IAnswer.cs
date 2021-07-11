using System;

namespace NG.ServiceWorker.SwForms
{
    public interface IAnswer
    {
        /// <summary>The raw answer.</summary>
        object AnswerObject { get; }

        /// <summary>The code value.</summary>
        string CodeValue { get; }

        /// <summary>The display value.</summary>
        string DisplayValue { get; }
    }
}
