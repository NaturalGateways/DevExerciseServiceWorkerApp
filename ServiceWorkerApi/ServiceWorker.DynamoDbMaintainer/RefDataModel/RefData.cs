using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.DynamoDbMaintainer.RefDataModel
{
    public class RefData
    {
        public RefDataType[] TypeArray { get; set; }

        public RefDataForm[] FormArray { get; set; }

    }

    public class RefDataType
    {
        public string TypeName { get; set; }

        public RefDataItem[] ItemArray { get; set; }
    }

    public class RefDataItem
    {
        public string ItemKey { get; set; }

        public string JsonData { get; set; }
    }
}
