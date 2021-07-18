using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.SwForms.Answers
{
    public class MultiAnswer : IAnswer
    {
        #region Base

        /// <summary>The answers wrapped in this answer object.</summary>
        private Dictionary<string, IAnswer> m_answersByCode = null;

        /// <summary>Constructor.</summary>
        public MultiAnswer(IAnswer originalAnswer, IAnswer newAnswer)
        {
            // Copy
            MultiAnswer originalMultiAnswer = originalAnswer as MultiAnswer;
            if (originalMultiAnswer != null)
            {
                m_answersByCode = originalMultiAnswer.m_answersByCode.ToDictionary(x => x.Key, y => y.Value);
            }
            else
            {
                m_answersByCode = new Dictionary<string, IAnswer>();
            }

            // Add
            m_answersByCode[newAnswer.CodeValue] = newAnswer;

            // Set strings
            this.CodeValue = string.Join(",", m_answersByCode.Keys);
            this.DisplayValue = string.Join(", ", m_answersByCode.Values.Select(x => x.DisplayValue));
        }

        /// <summary>Constructor.</summary>
        private MultiAnswer(Dictionary<string, IAnswer> answersByCodeValue)
        {
            m_answersByCode = answersByCodeValue;
            this.CodeValue = string.Join(",", m_answersByCode.Keys);
            this.DisplayValue = string.Join(", ", m_answersByCode.Values.Select(x => x.DisplayValue));
        }

        /// <summary>Getter for whether there is a specific answer in this set.</summary>
        public bool HasCodeValue(string codeValue)
        {
            return m_answersByCode.ContainsKey(codeValue);
        }

        /// <summary>Creates a new multi answer with the answer removed.</summary>
        public IAnswer MultiAnswerWithAnswerRemoved(string codeValue)
        {
            Dictionary<string, IAnswer> newAnswersByCode = m_answersByCode.Where(x => x.Key != codeValue).ToDictionary(x => x.Key, y => y.Value);
            if (newAnswersByCode.Any())
            {
                return new MultiAnswer(newAnswersByCode);
            }
            return NullAnswer.Null;
        }

        #endregion

        #region IAnswer implementation

        /// <summary>The raw answer.</summary>
        public object AnswerObject { get { return m_answersByCode; } }

        /// <summary>The code value.</summary>
        public string CodeValue { get; set; }

        /// <summary>The display value.</summary>
        public string DisplayValue { get; set; }

        #endregion
    }
}
