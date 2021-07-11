using System;

namespace NG.ServiceWorker
{
    public interface IApiService
    {
        /// <summary>Get the jobs.</summary>
        ApiModel.JobWithLinks[] GetJobArray();

        /// <summary>Get the form design of a job.</summary>
        ApiModel.FormIOModel.FormDesign GetJobFormDesign();
    }
}
