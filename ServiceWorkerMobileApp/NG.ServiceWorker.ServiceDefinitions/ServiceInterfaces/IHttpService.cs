using System;

namespace NG.ServiceWorker
{
    public interface IHttpConnection : IDisposable
    {
        /// <summary>Getter for a JSON from an HTTP endpoint.</summary>
        JsonType GetJson<JsonType>(string url);
    }

    public interface IHttpService
    {
        /// <summary>Getter for a HTTP connection.</summary>
        IHttpConnection GetConnection(string domainUrl);
    }
}
