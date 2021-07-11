using System;

namespace NG.ServiceWorker.SwForms.Answers
{
    public class StringAnswer : IAnswer
    {
        #region Base

        /// <summary>Constructor.</summary>
        public StringAnswer(string stringValue)
        {
            this.CodeValue = stringValue;
        }

        #endregion

        #region IAnswer implementation

        /// <summary>The raw answer.</summary>
        public object AnswerObject { get { return this.CodeValue; } }

        /// <summary>The code value.</summary>
        public string CodeValue { get; set; }

        /// <summary>The display value.</summary>
        public string DisplayValue { get { return this.CodeValue; } }

        #endregion
    }
}
