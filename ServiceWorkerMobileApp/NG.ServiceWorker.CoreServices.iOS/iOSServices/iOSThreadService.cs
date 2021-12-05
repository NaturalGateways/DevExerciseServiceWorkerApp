using System;
using System.Collections.Generic;
using System.Threading;

using Foundation;
using UIKit;

namespace NG.ServiceWorker.CoreServices.iOSServices
{
    public class iOSThreadService : BaseThreadService
    {
        #region BaseThreadService implementation

        /// <summary>Runs the given action on the main thread.</summary>
        public override void RunActionOnMainThread(Action action)
        {
            if (NSThread.Current.IsMainThread)
            {
                action.Invoke();
            }
            else
            {
                UIApplication.SharedApplication.InvokeOnMainThread(action);
            }
        }

        #endregion
    }
}
