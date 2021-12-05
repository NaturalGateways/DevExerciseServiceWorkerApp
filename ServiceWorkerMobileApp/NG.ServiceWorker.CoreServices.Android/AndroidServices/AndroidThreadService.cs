using System;
using System.Collections.Generic;

using Android.OS;

namespace NG.ServiceWorker.CoreServices.AndroidServices
{
    public class AndroidThreadService : BaseThreadService
    {
        #region BaseThreadService implementation

        /// <summary>Runs the given action on the main thread.</summary>
        public override void RunActionOnMainThread(Action action)
        {
            if (Looper.MyLooper() == Looper.MainLooper)
            {
                action.Invoke();
            }
            else
            {
                // Waiting async functions is usually bad, but all calls to this function are expecting the call to block until the UI thread code is done.
                Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(action).Wait();
            }
        }

        #endregion
    }
}
