using System;

namespace NG.ServiceWorker.ApiModel
{
    public class FormDesignSummary
    {
        public DateTime LastModified { get; set; }

        public string[] forms { get; set; }
    }

    public class FormDesign
    {
        public string FormType { get; set; }

        public string Key { get; set; }

        public string Url { get; set; }
    }
}
