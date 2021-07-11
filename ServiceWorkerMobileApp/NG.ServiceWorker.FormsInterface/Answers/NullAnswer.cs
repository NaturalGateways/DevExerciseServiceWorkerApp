using System;

namespace NG.ServiceWorker.SwForms.Answers
{
    public class NullAnswer : IAnswer
    {
        #region Base

        /// <summary>Singleton for a null answer.</summary>
        public static IAnswer Null { get; private set; } = new NullAnswer();

        #endregion

        #region IAnswer implementation

        /// <summary>The raw answer.</summary>
        public object AnswerObject { get { return null; } }

        /// <summary>The code value.</summary>
        public string CodeValue { get { return null; } }

        /// <summary>The display value.</summary>
        public string DisplayValue { get { return null; } }

        #endregion
    }
}
