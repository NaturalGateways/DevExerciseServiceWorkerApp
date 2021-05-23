using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultHttpService : IHttpService
    {
        #region Base

        private Dictionary<string, DefaultHttpConnection> m_connectionsByDomainUrl = new Dictionary<string, DefaultHttpConnection>();

        #endregion

        #region IApiService implementations

        /// <summary>Getter for a HTTP connection.</summary>
        public IHttpConnection GetConnection(string domainUrl)
        {
            Uri uri = new Uri(domainUrl);
            string domainUrlParsed = uri.Host;
            lock (m_connectionsByDomainUrl)
            {
                if (m_connectionsByDomainUrl.ContainsKey(domainUrlParsed))
                {
                    DateTime now = DateTime.Now;
                    DefaultHttpConnection existingConnection = m_connectionsByDomainUrl[domainUrlParsed];
                    if (existingConnection.ExpiryDateTime < now)
                    {
                        m_connectionsByDomainUrl.Remove(domainUrlParsed);
                        existingConnection.Dispose();
                    }
                    else
                    {
                        existingConnection.ExpiryDateTime = now.AddMinutes(15);
                        return existingConnection;
                    }
                }
                DefaultHttpConnection newConnection = new DefaultHttpConnection();
                m_connectionsByDomainUrl.Add(domainUrl, newConnection);
                return newConnection;
            }
        }

        #endregion
    }
}
