using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UIKit;

namespace NG.ServiceWorker.UIServices.CoreUIServices
{
    public class iOSUserInterfaceDialogService : IUserInterfaceDialogService
    {
        #region Base

        /// <summary>Getter for the currenltly shown view controller.</summary>
        private static UIViewController TopViewController
        {
            get
            {
                UIViewController topViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (topViewController.PresentedViewController != null)
                {
                    topViewController = topViewController.PresentedViewController;
                }
                return topViewController;
            }
        }

        #endregion

        #region IUserInterfaceDialogService implementation

        /// <summary>Displays a simple prompt to the user.</summary>
        public Task ShowMessageAsync(string title, string message)
        {
            Services.ThreadService.RunActionOnMainThread(() =>
            {
                UIAlertController alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                TopViewController.PresentViewController(alertController, true, null);
            });
            return Task.CompletedTask;
        }

        #endregion
    }
}
