using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;

namespace NG.ServiceWorker.UIServices.CoreUIServices
{
    public class AndroidUserInterfaceDialogService : IUserInterfaceDialogService
    {
        #region Base

        /// <summary>The current context.</summary>
        private Context Context { get { return (Context)Platform.GetPlatform().GetOsObject(Platform.OS_ANDROID_CONTEXT); } }

        #endregion

        #region IUserInterfaceDialogService implementation

        /// <summary>Displays a simple prompt to the user.</summary>
        public Task ShowMessageAsync(string title, string message)
        {
            // Create alert
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Context);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => { });

            // Create and display dialog
            Services.ThreadService.RunActionOnMainThread(() => {
                Dialog dialog = alert.Create();
                dialog.Show();
            });

            // Return
            return Task.CompletedTask;
        }

        #endregion
    }
}
