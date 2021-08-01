using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.Converter
{
    public static class LoadDataModel
    {
        public static void Load(object data, FormsIoDocument document)
        {
            // Get fields by key
            IJsonObject jsonObject = Services.JsonService.JsonObjectFromAnonObject(data);

            // Traverse fields
            foreach (FormsIoField field in document.FieldsByKey.Values)
            {
                IJsonObject fieldObject = jsonObject.GetDictionaryObject(field.FieldDesign.key);
                field.AnswerModel.Answer = CreateAnswerFromFieldObject(fieldObject);
            }
        }

        /// <summary>Creates an answer from a field object.</summary>
        private static SwForms.IAnswer CreateAnswerFromFieldObject(IJsonObject fieldObject)
        {
            switch (fieldObject.ObjectType)
            {
                case JsonObjectType.Null:
                    return SwForms.Answers.NullAnswer.Null;
                case JsonObjectType.String:
                    return new SwForms.Answers.StringAnswer(fieldObject.AsString);
                case JsonObjectType.Array:
                    {
                        SwForms.IAnswer[] answerArray = fieldObject.AsArray.Select(x => CreateAnswerFromFieldObject(x)).ToArray();
                        return new SwForms.Answers.MultiAnswer(answerArray);
                    }
                default:
                    return SwForms.Answers.NullAnswer.Null;
            }
        }
    }
}
