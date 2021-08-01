using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.SelectLists
{
    public class FormValuesSelectList : SwForms.ISelectList
    {
        #region Base

        /// <summary>The lazy-loaded answers if this is a single level select list.</summary>
        private ApiModel.FormIOModel.FormDesignComponentValue[] m_valueArray = null;

        /// <summary>The lazy-loaded answers if this is a single level select list.</summary>
        private SwForms.ISelectListLevel m_flatLevel = null;

        /// <summary>Constructor.</summary>
        public FormValuesSelectList(ApiModel.FormIOModel.FormDesignComponentValue[] valueArray)
        {
            m_valueArray = valueArray;
        }

        #endregion

        #region SwForms.ISelectList implementation

        /// <summary>The select type.</summary>
        public SwForms.SelectListSelectType SelectType { get; }

        /// <summary>The lazy-loaded answers if this is a single level select list.</summary>
        public SwForms.ISelectListLevel FlatLevel
        {
            get
            {
                if (m_flatLevel == null)
                    m_flatLevel = new FormValuesSelectListLevel(m_valueArray);
                return m_flatLevel;
            }
        }

        #endregion
    }

    public class FormValuesSelectListLevel : SwForms.ISelectListLevel
    {
        #region Base

        /// <summary>Constructor.</summary>
        public FormValuesSelectListLevel(ApiModel.FormIOModel.FormDesignComponentValue[] valueArray)
        {
            // Create selectable answers
            this.SelectableAnswers = valueArray.Select(x => new FormValuesAnswer(x)).ToArray();
        }

        #endregion

        #region SwForms.ISelectListLevel implementation

        /// <summary>The selectable answers.</summary>
        public IEnumerable<SwForms.IAnswer> SelectableAnswers { get; private set; }

        #endregion
    }

    public class FormValuesAnswer : SwForms.IAnswer
    {
        #region Base

        /// <summary>The value being wrapped.</summary>
        private ApiModel.FormIOModel.FormDesignComponentValue m_value = null;

        /// <summary>Constructor.</summary>
        public FormValuesAnswer(ApiModel.FormIOModel.FormDesignComponentValue value)
        {
            m_value = value;
        }

        #endregion

        #region SwForms.IAnswer implementation

        /// <summary>The raw answer.</summary>
        public object AnswerObject { get { return m_value.value; } }

        /// <summary>The code value.</summary>
        public string CodeValue { get { return m_value.value; } }

        /// <summary>The display value.</summary>
        public string DisplayValue { get { return m_value.label; } }

        #endregion
    }
}
