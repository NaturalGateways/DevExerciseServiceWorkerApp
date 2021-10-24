using System;

namespace NG.ServiceWorker.DataModel
{
    public class ContactSummary
    {
        public string ContactId { get; set; }

        public string FullName { get; set; }

        public string BusinessName { get; set; }

        public string Address { get; set; }
    }

    public class Contact
    {
        public string ContactId { get; set; }

        public string FullName { get; set; }

        public string BusinessName { get; set; }

        public string Address { get; set; }

        public object Data { get; set; }
    }
}
