using System;

namespace NG.ServiceWorker.DataModel
{
    public class FormDesign
    {
        public string FormType { get; set; }

        public string FormKey { get; set; }

        public ApiModel.FormIOModel.FormDesign FormsIO { get; set; }
    }
}
