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

        public bool multiple { get; set; } = false;

        public bool disabled { get; set; }

        public string defaultValue { get; set; }

        public Dictionary<string, string> attributes { get; set; }

        public FormDesignComponentData data { get; set; }

        public FormDesignComponentValidation validate { get; set; }

        public FormDesignComponentConditional conditional { get; set; }

        public FormDesignComponentValue[] values { get; set; }

        public FormDesignComponent[] components { get; set; }
    }

    public class FormDesignComponentData
    {
        public FormDesignComponentValue[] values { get; set; }
    }

    public class FormDesignComponentValidation
    {
        public bool required { get; set; }
    }

    public class FormDesignComponentConditional
    {
        public object json { get; set; }
    }

    public class FormDesignComponentValue
    {
        public string label { get; set; }

        public string value { get; set; }
    }
}
