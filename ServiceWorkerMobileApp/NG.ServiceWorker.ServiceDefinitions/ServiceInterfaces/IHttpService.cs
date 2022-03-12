using System;

namespace NG.ServiceWorker
{
    public interface IHttpConnection : IDisposable
    {
        /// <summary>Getter for a string from an HTTP endpoint.</summary>
        string GetString(string url);

        /// <summary>Getter for a JSON from an HTTP endpoint.</summary>
        JsonType GetJson<JsonType>(string url);
    }

    public interface IHttpService
    {
        /// <summary>Encodes a string to be URL friendly.</summary>
        string EncodeUrl(string rawText);

        /// <summary>Getter for a HTTP connection.</summary>
        IHttpConnection GetConnection(string domainUrl);
    }
}
