using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultApiService : IApiService
    {
        #region Base

        private IHttpConnection ApiConnection { get { return Services.HttpService.GetConnection("http://files.naturalgateways.com"); } }

        private IHttpConnection FormsIoConnection { get { return Services.HttpService.GetConnection("http://thnwvtixxzqkrmr.form.io"); } }

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


        /// <summary>Get the form design of a job.</summary>
        public ApiModel.FormIOModel.FormDesign GetJobFormDesign()
        {
            string url = "https://thnwvtixxzqkrmr.form.io/cleanerjob?_ga=2.241665346.1506319149.1625382416-2110815082.1623673278";
            return this.FormsIoConnection.GetJson<ApiModel.FormIOModel.FormDesign>(url);
        }

        #endregion
    }
}
