using System;

namespace NG.ServiceWorker.UIServices
{
    public interface IUserInterfaceActiveDataService
    {
        /// <summary>The list of contacts.</summary>
        UIModel.ContactListModel ContactList { get; }
    }
}
