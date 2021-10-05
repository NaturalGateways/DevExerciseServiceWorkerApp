using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.DynamoDbMaintainer
{
    public static class CleanerGenerator
    {
        public static RefDataModel.RefData CreateRefData()
        {
            return new RefDataModel.RefData
            {
                TypeArray = Array.Empty<RefDataModel.RefDataType>(),
                FormArray = new RefDataModel.RefDataForm[]
                {
                    new RefDataModel.RefDataForm { FormType = "FormsIO", Key = "contact", Url = "https://thnwvtixxzqkrmr.form.io/cleanercontact" }
                }
            };
        }
    }
}
