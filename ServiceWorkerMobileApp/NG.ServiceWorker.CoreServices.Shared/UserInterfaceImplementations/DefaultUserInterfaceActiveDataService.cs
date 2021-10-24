using System;
using System.Linq;

namespace NG.ServiceWorker.UIServices.CoreUIServices
{
    public class DefaultUserInterfaceActiveDataService : IUserInterfaceActiveDataService
    {
        #region Base

        /// <summary>Constructor.</summary>
        public DefaultUserInterfaceActiveDataService()
        {
            // Load the contacts
            this.ContactList.ContactList = Services.MainDataService.GetEntitys<DataModel.ContactSummary>(EntityType.Contact).Select(x => new UIModel.ContactListContactModel(x)).ToList();
        }

        #endregion

        #region IUserInterfaceActiveDataService implementation

        /// <summary>The list of contacts.</summary>
        public UIModel.ContactListModel ContactList { get; private set; } = new UIModel.ContactListModel();

        #endregion
    }
}
