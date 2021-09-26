using System;
using System.Linq;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultApiService : IApiService
    {
        #region Base

        private string API_DOMAIN = "https://dev-api-sw.naturalgateways.com";

        private IHttpConnection ApiConnection { get { return Services.HttpService.GetConnection(API_DOMAIN); } }

        private IHttpConnection S3Connection { get { return Services.HttpService.GetConnection("http://files.naturalgateways.com"); } }

        private IHttpConnection FormsIoConnection { get { return Services.HttpService.GetConnection("http://thnwvtixxzqkrmr.form.io"); } }

        #endregion

        #region API response handling

        private class ApiResponseContainer<ResponseType>
        {
            public bool Success { get; set; }

            public ResponseType Response { get; set; }

            public ApiResponseException[] ErrorException { get; set; }
        }

        private class ApiResponseException
        {
            public string TypeName { get; set; }

            public string Message { get; set; }

            public string StackTrace { get; set; }
        }

        private JsonType GetApiJson<JsonType>(string url)
        {
            ApiResponseContainer<JsonType> response = this.ApiConnection.GetJson<ApiResponseContainer<JsonType>>(url);
            if (response.Success)
            {
                return response.Response;
            }
            if (response.ErrorException?.Any() ?? false)
            {
                throw new Exception($"API Error: {response.ErrorException.First().Message}");
            }
            throw new Exception("Unknown API error.");
        }

        #endregion

        #region IApiService implementations

        /// <summary>Get for the last modified date times from the server.</summary>
        public ApiModel.LastMod GetLastModifiedDateTimes()
        {
            string url = $"{API_DOMAIN}/anon?RequestType=GetLastMod";
            return GetApiJson<ApiModel.LastMod>(url);
        }

        /// <summary>Get for some reference data.</summary>
        public ApiModel.RefData.JobStatus[] GetJobStatuses()
        {
            string url = $"{API_DOMAIN}/anon?RequestType=GetRefData&ItemType=JobStatus";
            return GetApiJson<ApiModel.RefData.JobStatus[]>(url);
        }

        /// <summary>Get for some reference data.</summary>
        public ApiModel.RefData.PaymentType[] GetPaymentTypes()
        {
            string url = $"{API_DOMAIN}/anon?RequestType=GetRefData&ItemType=PaymentType";
            return GetApiJson<ApiModel.RefData.PaymentType[]>(url);
        }

        /// <summary>Get for some reference data.</summary>
        public ApiModel.RefData.MaterialType[] GetMaterialTypes()
        {
            string url = $"{API_DOMAIN}/anon?RequestType=GetRefData&ItemType=MaterialType";
            return GetApiJson<ApiModel.RefData.MaterialType[]>(url);
        }

        /// <summary>Get the jobs.</summary>
        public ApiModel.JobWithLinks[] GetJobArray()
        {
            string url = "https://https.naturalgateways.com/ServiceWorker/Jobs3.json";
            ApiModel.JobWithLinks[] data = this.S3Connection.GetJson<ApiModel.JobWithLinks[]>(url);
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
