using System;
using System.Threading.Tasks;

namespace NG.ServiceWorker.UIServices
{
    public interface IUserInterfaceDialogService
    {
        /// <summary>Displays a simple prompt to the user.</summary>
        Task ShowMessageAsync(string title, string message);
    }
}
