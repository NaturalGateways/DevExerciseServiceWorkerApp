using System;

namespace NG.ServiceWorker
{
    public interface IJsonService
    {
        /// <summary>Deserialises JSON.</summary>
        JsonType DeserialiseString<JsonType>(string jsonString);

        /// <summary>Deserialises JSON.</summary>
        JsonType DeserialiseStream<JsonType>(System.IO.Stream jsonStream);
    }
}
