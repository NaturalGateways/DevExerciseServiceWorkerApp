using System;

namespace NG.ServiceWorker.SwForms.Answers
{
    public class DateTimeAnswer : IAnswer
    {
        #region Base

        /// <summary>The date time value.</summary>
        public DateTime UtcValue { get; private set; }
        /// <summary>The date time value.</summary>
        public DateTime LocalValue { get; private set; }

        /// <summary>Constructor.</summary>
        public DateTimeAnswer(DateTime dateTimeValue)
        {
            this.UtcValue = dateTimeValue.ToUniversalTime();
            this.LocalValue = dateTimeValue.ToLocalTime();
        }

        #endregion

        #region IAnswer implementation

        /// <summary>The raw answer.</summary>
        public object AnswerObject { get { return this.CodeValue; } }

        /// <summary>The code value.</summary>
        public string CodeValue { get { return this.UtcValue.ToString("o"); } }

        /// <summary>The display value.</summary>
        public string DisplayValue { get { return this.LocalValue.ToString(); } }

        #endregion
    }
}
