using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.Rules
{
    public class ConditionalRule : SwForms.IRule
    {
        #region Base

        /// <summary>The rule effect.</summary>
        public IRuleEffect RuleEffect { get; private set; }

        /// <summary>The JSON settings of the rule.</summary>
        private IJsonObject m_jsonObject = null;

        /// <summary>Constructor.</summary>
        public ConditionalRule(IRuleEffect ruleEffect, object rulesJsonObj)
        {
            this.RuleEffect = ruleEffect;
            m_jsonObject = Services.JsonService.JsonObjectFromAnonObject(rulesJsonObj);

            // Traverse rule JSON for causes
            CheckJsonForFields(m_jsonObject);
        }

        /// <summary>Called to set the initial value of the rule,</summary>
        public void Initialise()
        {
            this.RuleEffect.RuleEffectIsVisible = CalculateRule(m_jsonObject);
        }

        /// <summary>Checks a JSON object for cause fields.</summary>
        private void CheckJsonForFields(IJsonObject jsonObject)
        {
            // Check AND
            if (jsonObject.HasDictionaryKey("and"))
            {
                IJsonObject andObject = jsonObject.GetDictionaryObject("and");
                if (andObject.ObjectType != JsonObjectType.Array)
                {
                    return;
                }
                foreach (IJsonObject andChild in andObject.AsArray)
                {
                    CheckJsonForFields(andChild);
                }
            }
            // Check equals
            else if (jsonObject.HasDictionaryKey("=="))
            {
                IJsonObject equalsObject = jsonObject.GetDictionaryObject("==");
                if (equalsObject.ObjectType != JsonObjectType.Array)
                {
                    return;
                }
                IJsonObject[] equalsChildren = equalsObject.AsArray;
                if (equalsChildren.Length != 2)
                {
                    return;
                }
                CheckValueForFields(equalsChildren[0]);
                CheckValueForFields(equalsChildren[1]);
            }
        }

        /// <summary>Checks a JSON object for cause fields.</summary>
        private void CheckValueForFields(IJsonObject jsonObject)
        {
            // Check variable reference to field
            if (jsonObject.ObjectType == JsonObjectType.Dictionary && jsonObject.HasDictionaryKey("var"))
            {
                string varName = jsonObject.GetDictionaryObject("var").AsString;
                if (string.IsNullOrEmpty(varName) || m_causeRefByVar.ContainsKey(varName))
                {
                    return;
                }
                if (varName.StartsWith("data.") && m_causeRefByVar.ContainsKey(varName) == false)
                {
                    CauseReference causeRef = new CauseReference { Key = varName.Substring(5) };
                    this.CauseReferenceList.Add(causeRef);
                    m_causeRefByVar.Add(varName, causeRef);
                }
            }
        }

        #endregion

        #region Cause References

        /// <summary>A reference to the cause.</summary>
        public class CauseReference
        {
            /// <summary>The field being referenced.</summary>
            public string Key { get; set; }

            /// <summary>The field being referenced.</summary>
            public FormsIoField Field { get; set; }
        }

        /// <summary>The cause fields indexed by their key.</summary>
        public List<CauseReference> CauseReferenceList { get; private set; } = new List<CauseReference>();
        /// <summary>The cause fields indexed by their key.</summary>
        private Dictionary<string, CauseReference> m_causeRefByVar = new Dictionary<string, CauseReference>();

        #endregion

        #region Calculation

        /// <summary>Calculates a boolean result.</summary>
        private bool CalculateRule(IJsonObject jsonObject)
        {
            // Check AND
            if (jsonObject.HasDictionaryKey("and"))
            {
                IJsonObject andObject = jsonObject.GetDictionaryObject("and");
                if (andObject.ObjectType != JsonObjectType.Array)
                {
                    return false;
                }
                foreach (IJsonObject andChild in andObject.AsArray)
                {
                    if (CalculateRule(andChild) == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            // Check equals
            if (jsonObject.HasDictionaryKey("=="))
            {
                IJsonObject equalsObject = jsonObject.GetDictionaryObject("==");
                if (equalsObject.ObjectType != JsonObjectType.Array)
                {
                    return false;
                }
                IJsonObject[] equalsChildren = equalsObject.AsArray;
                if (equalsChildren.Length != 2)
                {
                    return false;
                }
                return CalculateValue(equalsChildren[0]) == CalculateValue(equalsChildren[1]);
            }
            // Return false if we have an invalid JSON
            return false;
        }

        /// <summary>Calculates a boolean result.</summary>
        private string CalculateValue(IJsonObject jsonObject)
        {
            // Check string
            if (jsonObject.ObjectType == JsonObjectType.String)
            {
                return jsonObject.AsString;
            }
            // Check variable reference to field
            if (jsonObject.ObjectType == JsonObjectType.Dictionary && jsonObject.HasDictionaryKey("var"))
            {
                string varName = jsonObject.GetDictionaryObject("var").AsString;
                if (string.IsNullOrEmpty(varName) || m_causeRefByVar.ContainsKey(varName) == false)
                {
                    return null;
                }
                return m_causeRefByVar[varName].Field.AnswerModel.Answer.CodeValue;
            }
            // Else unkown
            return null;
        }

        #endregion

        #region SwForms.IRule implementation

        /// <summary>Called when a rule cause changes so that the rule needs to retrigger,</summary>
        public void OnCauseChanged()
        {
            // Set visibility
            this.RuleEffect.RuleEffectIsVisible = CalculateRule(m_jsonObject);

            // Update UI
            this.RuleEffect.EffectModel.OnDataChanged();
        }

        #endregion
    }
}
