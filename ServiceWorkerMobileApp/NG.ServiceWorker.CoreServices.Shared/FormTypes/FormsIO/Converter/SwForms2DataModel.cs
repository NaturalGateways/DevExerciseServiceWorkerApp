using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.FormTypes.FormsIO.Converter
{
    public static class SwForms2DataModel
    {
        public static object CreateData(SwForms.IFormDocument document)
        {
            // Create document
            FormsIoDocument ioDoc = (FormsIoDocument)document;

            // Create dictionary to return
            Dictionary<string, object> docData = new Dictionary<string, object>();

            // Loop through all the fields
            foreach (FormsIoField field in ioDoc.SectionList.SelectMany(x => x.FieldList))
            {
                docData[field.FieldDesign.key] = CreateDataFromField(field);
            }

            // Return
            return docData;
        }

        private static object CreateDataFromField(FormsIoField field)
        {
            return field.AnswerModel.Answer.AnswerObject;
        }
    }
}
