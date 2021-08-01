using System;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI
{
    public class FormsDateTimeFieldViewModel : FormsFieldViewModel
    {
        /// <summary>Constructor.</summary>
        public FormsDateTimeFieldViewModel(SwForms.IFormField formField)
            : base(formField)
        {
            //
        }

        /// <summary>The current date and time.</summary>
        public DateTime CurrentDateTime
        {
            get
            {
                SwForms.IAnswer answer = this.FormField.AnswerModel.Answer;
                SwForms.Answers.DateTimeAnswer dateTimeAnswer = answer as SwForms.Answers.DateTimeAnswer;
                if (dateTimeAnswer != null)
                {
                    return dateTimeAnswer.LocalValue;
                }
                if (string.IsNullOrEmpty(answer.CodeValue) == false)
                {
                    DateTime parsedDateTime;
                    if (DateTime.TryParse(answer.CodeValue, out parsedDateTime))
                    {
                        return parsedDateTime.ToLocalTime();
                    }
                }
                return DateTime.Now;
            }
        }

        /// <summary>The label text.</summary>
        public string LabelText { get { return this.FormField.Label; } }

        /// <summary>The date component of the value.</summary>
        public DateTime DateValue
        {
            get { return this.CurrentDateTime.Date; }
            set
            {
                DateTime oldValue = this.CurrentDateTime;
                DateTime newValue = new DateTime(value.Year, value.Month, value.Day, oldValue.Hour, oldValue.Minute, oldValue.Second, DateTimeKind.Local);
                this.FormField.AnswerModel.Answer = new SwForms.Answers.DateTimeAnswer(newValue);
                this.FormField.AnswerModel.OnDataChanged();
            }
        }

        /// <summary>The time component of the value.</summary>
        public TimeSpan TimeValue
        {
            get { return this.CurrentDateTime.TimeOfDay; }
            set
            {
                DateTime oldValue = this.CurrentDateTime;
                DateTime newValue = new DateTime(oldValue.Year, oldValue.Month, oldValue.Day, value.Hours, value.Minutes, value.Seconds, DateTimeKind.Local);
                this.FormField.AnswerModel.Answer = new SwForms.Answers.DateTimeAnswer(newValue);
                this.FormField.AnswerModel.OnDataChanged();
            }
        }
    }
}
