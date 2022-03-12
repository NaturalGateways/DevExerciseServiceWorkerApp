using System;
using System.Net.Http;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultHttpConnection : IHttpConnection
    {
        #region Base

        /// <summary>The HTTP client.</summary>
        private HttpClient m_httpClient = new HttpClient();

        /// <summary>The time that this connection expires.</summary>
        public DateTime ExpiryDateTime { get; set; }

        /// <summary>Constructor.</summary>
        public DefaultHttpConnection()
        {
            this.ExpiryDateTime = DateTime.Now.AddMinutes(15);
        }

        #endregion

        #region Dispose implementation

        /// <summary>Dispose of the web client.</summary>
        public void Dispose()
        {
            lock (m_httpClient)
            {
                if (m_httpClient != null)
                {
                    m_httpClient.Dispose();
                    m_httpClient = null;
                }
            }
        }

        #endregion

        #region IApiService implementations

        /// <summary>Getter for a string from an HTTP endpoint.</summary>
        public string GetString(string url)
        {
            lock (m_httpClient)
            {
                return m_httpClient.GetStringAsync(url).Result;
            }
        }

        /// <summary>Getter for a JSON from an HTTP endpoint.</summary>
        public JsonType GetJson<JsonType>(string url)
        {
            lock (m_httpClient)
            {
                System.IO.Stream responseStream = m_httpClient.GetStreamAsync(url).Result;
                return Services.JsonService.DeserialiseStream<JsonType>(responseStream);
            }
        }

        #endregion
    } 
}
