using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultApiService : IApiService
    {
        #region Base

        private IHttpConnection ApiConnection { get { return Services.HttpService.GetConnection("http://files.naturalgateways.com"); } }

        #endregion

        #region IApiService implementations

        /// <summary>Get the jobs.</summary>
        public ApiModel.JobWithLinks[] GetJobArray()
        {
            string url = "https://https.naturalgateways.com/ServiceWorker/Jobs3.json";
            ApiModel.JobWithLinks[] data = this.ApiConnection.GetJson<ApiModel.JobWithLinks[]>(url);
            for (int itemIndex = 0; itemIndex != data.Length; ++itemIndex)
            {
                data[itemIndex].Job.JobKey = "J" + (itemIndex + 1).ToString("D3");
            }
            return data;
        }

        #endregion
    }
}
