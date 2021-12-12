using System;
using System.Collections.Generic;
using System.Linq;

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

            // Do rule triggering
            if (field.RuleCauses?.Any() ?? false)
            {
                foreach (IRule ruleCause in field.RuleCauses)
                {
                    ruleCause.OnCauseChanged();
                }
            }

            // Update listeners
            field.AnswerModel.OnDataChanged();
        }
    }
}
