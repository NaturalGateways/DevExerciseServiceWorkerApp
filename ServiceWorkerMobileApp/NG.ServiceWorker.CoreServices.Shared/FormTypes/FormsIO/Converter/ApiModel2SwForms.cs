using System;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.Converter
{
    public static class ApiModel2SwForms
    {
        public static FormsIoDocument CreateDocument(ApiModel.FormIOModel.FormDesign formDesign)
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

            // Return
            return doc;
        }

        private static FormsIoSection CreateSection(FormsIoDocument doc, ApiModel.FormIOModel.FormDesignComponent sectionComponent)
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

            // Return
            return sectionNode;
        }

        private static FormsIoField CreateField(ApiModel.FormIOModel.FormDesignComponent fieldComponent)
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
