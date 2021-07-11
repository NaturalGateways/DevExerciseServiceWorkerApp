using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.ApiModel.FormIOModel
{
    public class FormDesign
    {
        public string name { get; set; }

        public FormDesignComponent[] components { get; set; }
    }

    public class FormDesignComponent
    {
        public string key { get; set; }

        public string title { get; set; }

        public string type { get; set; }

        public string label { get; set; }

        public bool multiple { get; set; }

        public bool disabled { get; set; }

        public Dictionary<string, string> attributes { get; set; }

        public FormDesignComponentData data { get; set; }

        public FormDesignComponentValue[] values { get; set; }

        public FormDesignComponent[] components { get; set; }
    }

    public class FormDesignComponentData
    {
        public FormDesignComponentValue[] values { get; set; }
    }

    public class FormDesignComponentValue
    {
        public string label { get; set; }

        public string value { get; set; }
    }
}
