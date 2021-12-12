using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.Converter
{
    public class ApiModel2SwForms
    {
        /// <summary>Static facade.</summary>
        public static FormsIoDocument CreateDocument(ApiModel.FormIOModel.FormDesign formDesign)
        {
            ApiModel2SwForms converter = new ApiModel2SwForms();
            return converter.CreateDocumentFromApiModel(formDesign);
        }

        /// <summary>The list of conditional rules created.</summary>
        private List<Rules.ConditionalRule> m_conditionalRuleList = new List<Rules.ConditionalRule>();

        private FormsIoDocument CreateDocumentFromApiModel(ApiModel.FormIOModel.FormDesign formDesign)
        {
            // Create document
            FormsIoDocument doc = new FormsIoDocument(formDesign);

            // Read sections
            if (formDesign.components != null)
            {
                foreach (ApiModel.FormIOModel.FormDesignComponent component in formDesign.components)
                {
                    doc.SectionList.Add(CreateSection(doc, component));
                }
            }

            // Initialise rules
            foreach (Rules.ConditionalRule conditionalRule in m_conditionalRuleList)
            {
                foreach (Rules.ConditionalRule.CauseReference fieldRef in conditionalRule.CauseReferenceList)
                {
                    if (doc.FieldsByKey.ContainsKey(fieldRef.Key))
                    {
                        fieldRef.Field = doc.FieldsByKey[fieldRef.Key];
                        if (fieldRef.Field.CauseRuleList == null)
                            fieldRef.Field.CauseRuleList = new List<SwForms.IRule>();
                        fieldRef.Field.CauseRuleList.Add(conditionalRule);
                    }
                }
                conditionalRule.Initialise();
            }

            // Return
            return doc;
        }

        private FormsIoSection CreateSection(FormsIoDocument doc, ApiModel.FormIOModel.FormDesignComponent sectionComponent)
        {
            // Create document
            FormsIoSection sectionNode = new FormsIoSection(sectionComponent);

            // Read sections
            if (sectionComponent.components != null)
            {
                foreach (ApiModel.FormIOModel.FormDesignComponent component in sectionComponent.components)
                {
                    FormsIoField newField = CreateField(component);
                    sectionNode.FieldList.Add(newField);
                    if (doc.FieldsByKey.ContainsKey(newField.FieldDesign.key) == false)
                    {
                        doc.FieldsByKey.Add(newField.FieldDesign.key, newField);
                    }
                }
            }

            // Read rules
            if (sectionComponent.conditional?.json != null)
            {
                Rules.ConditionalRule conditionalRule = new Rules.ConditionalRule(sectionNode, sectionComponent.conditional.json);
                m_conditionalRuleList.Add(conditionalRule);
            }

            // Return
            return sectionNode;
        }

        private FormsIoField CreateField(ApiModel.FormIOModel.FormDesignComponent fieldComponent)
        {
            // Create document
            FormsIoField fieldNode = new FormsIoField(fieldComponent);

            // Check for default value
            if (string.IsNullOrEmpty(fieldComponent.defaultValue) == false)
            {
                fieldNode.AnswerModel.Answer = new SwForms.Answers.StringAnswer(fieldComponent.defaultValue);
            }

            // Return
            return fieldNode;
        }
    }
}
