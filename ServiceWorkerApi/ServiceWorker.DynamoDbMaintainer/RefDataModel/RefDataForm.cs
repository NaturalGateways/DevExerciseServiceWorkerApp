using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.DynamoDbMaintainer.RefDataModel
{
    public class RefDataForm
    {
        public string FormType { get; set; }

        public string Key { get; set; }

        public string Url { get; set; }
    }
}
