using System;

namespace NG.ServiceWorker
{
    public interface IJsonService
    {
        /// <summary>Serialises an object into JSON.</summary>
        string SerialiseObject(object jsonObject);

        /// <summary>Deserialises JSON.</summary>
        JsonType DeserialiseString<JsonType>(string jsonString);

        /// <summary>Deserialises JSON.</summary>
        JsonType DeserialiseStream<JsonType>(System.IO.Stream jsonStream);
    }
}
