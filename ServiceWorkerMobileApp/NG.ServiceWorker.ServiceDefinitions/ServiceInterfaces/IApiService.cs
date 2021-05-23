using System;

namespace NG.ServiceWorker
{
    public interface IApiService
    {
        /// <summary>Get the jobs.</summary>
        ApiModel.Job[] GetJobArray();
    }
}
