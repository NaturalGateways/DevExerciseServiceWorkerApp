using System;
using System.Threading;

namespace NG.ServiceWorker.CoreServices.Sync
{
    public class PeriodicDownloadSyncThread
    {
        /// <summary>The logger for all syncing under this thread.</summary>
        public static ILogger Logger { get { return Services.LogService.CreateLogger("PeriodicDownloadSync"); } }

        /// <summary>The steps to run.</summary>
        private IDownloadStep[] m_stepArray = null;

        /// <summary>The thread running the sync.</summary>
        private Thread m_thread = null;

        /// <summary>Boolean set to false to terminate the thread.</summary>
        private bool m_shouldRun = true;

        /// <summary>Date time for when to next run.</summary>
        private DateTime m_nextPeriod = DateTime.Now;

        /// <summary>Constructor.</summary>
        public PeriodicDownloadSyncThread()
        {
            // The steps to run
            m_stepArray = new IDownloadStep[]
            {
                new PeriodicDownloadSteps.JobStatusStep(),
                new PeriodicDownloadSteps.PaymentTypeStep(),
                new PeriodicDownloadSteps.FormsStep()
            };

            // Create thread
            m_thread = new Thread(new ThreadStart(ThreadImplementation));
            m_thread.Start();
        }

        /// <summary>The implementation of the thread.</summary>
        private void ThreadImplementation()
        {
            while (m_shouldRun)
            {
                // Check if we should run
                DateTime now = DateTime.Now;
                if (now < m_nextPeriod)
                {
                    int millisSleep = (int)m_nextPeriod.Subtract(now).TotalMilliseconds;
                    Thread.Sleep(Math.Min(millisSleep, 5 * 60 * 1000));
                }

                // Try catch for error handling and reporting
                try
                {
                    // Downlaod last modified times
                    ApiModel.LastMod lastMod = Services.ApiService.GetLastModifiedDateTimes();

                    // Run steps
                    foreach (IDownloadStep step in m_stepArray)
                    {
                        step.DownloadStep_ExecuteUpdate(lastMod);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Unexpected error on periodic download sync.", ex);
                }

                // Wait an hour before checking again
                m_nextPeriod = DateTime.Now.AddHours(1);
            }
        }
    }
}
