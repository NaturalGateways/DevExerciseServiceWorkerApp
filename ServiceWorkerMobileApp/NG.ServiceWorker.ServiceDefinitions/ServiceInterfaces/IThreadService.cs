using System;

namespace NG.ServiceWorker
{
    public interface IThreadService
    {
        /// <summary>Runs the given action on the main thread.</summary>
        void RunActionOnMainThread(Action action);

        /// <summary>Runs the given action on a background thread.</summary>
        void RunActionOnBackgroundThread(string threadName, Action action);
    }
}
