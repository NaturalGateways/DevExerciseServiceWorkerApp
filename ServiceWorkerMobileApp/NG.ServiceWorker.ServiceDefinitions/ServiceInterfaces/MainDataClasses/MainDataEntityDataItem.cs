using System;

namespace NG.ServiceWorker
{
    public class MainDataEntityDataItem
    {
        public Type DataType { get; private set; }

        public string DataKey { get; private set; }

        public object DataObject { get; private set; }

        public static MainDataEntityDataItem Create<DataType>(string dataKey, DataType dataObject)
        {
            return new MainDataEntityDataItem
            {
                DataType = typeof(DataType),
                DataKey = dataKey,
                DataObject = dataObject
            };
        }
    }
}
