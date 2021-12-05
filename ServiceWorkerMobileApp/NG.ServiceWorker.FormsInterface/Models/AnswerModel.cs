using System;

namespace NG.ServiceWorker.SwForms
{
    public class AnswerModel : UI.Model
    {
        /// <summary>The answer.</summary>
        public IAnswer Answer { get; set; } = Answers.NullAnswer.Null;

        /// <summary>The validation of the answer.</summary>
        public ValidationResult Validation { get; set; } = ValidationResult.Passed;
    }
}
