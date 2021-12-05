using System;
using System.Threading;

namespace NG.ServiceWorker.CoreServices
{
    public abstract class BaseThreadService : IThreadService
    {
        #region IThreadService implementation

        /// <summary>Runs the given action on the main thread.</summary>
        public abstract void RunActionOnMainThread(Action action);

        /// <summary>Runs the given action on a background thread.</summary>
        public void RunActionOnBackgroundThread(string threadName, Action action)
        {
            Thread thread = new Thread(new ThreadStart(action));
            thread.Name = threadName;
            thread.Start();
        }

        #endregion
    }
}
