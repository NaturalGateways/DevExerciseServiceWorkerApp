using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.SwForms
{
    public static class FormsHelper
    {
        /// <summary>Used to do all the needed actions when setting an answer.</summary>
        public static void SetAnswer(IFormField field, IAnswer answer)
        {
            // Set answer
            field.AnswerModel.Answer = answer;

            // Do validation
            IValidation validation = field.Validation;
            if (validation != null)
            {
                field.AnswerModel.Validation = validation.GetValidationResult();
            }

            // Update listeners
            field.AnswerModel.OnDataChanged();
        }
    }
}
