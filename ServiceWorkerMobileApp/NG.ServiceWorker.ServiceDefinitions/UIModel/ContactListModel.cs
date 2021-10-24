using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.UIModel
{
    public class ContactListContactModel : UI.Model
    {
        public DataModel.ContactSummary ContactSummary { get; set; }

        public ContactListContactModel(DataModel.ContactSummary contactSummary)
        {
            this.ContactSummary = contactSummary;
        }
    }

    public class ContactListModel : UI.Model
    {
        public List<ContactListContactModel> ContactList { get; set; }
    }
}
