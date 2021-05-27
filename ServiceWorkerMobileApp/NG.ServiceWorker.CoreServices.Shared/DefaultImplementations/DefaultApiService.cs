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
        public ApiModel.Job[] GetJobArray()
        {
            string url = "https://https.naturalgateways.com/ServiceWorker/Jobs.json";
            return this.ApiConnection.GetJson<ApiModel.Job[]>(url);
        }

        #endregion
    }
}
