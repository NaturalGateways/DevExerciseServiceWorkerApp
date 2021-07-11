using System;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultJsonService : IJsonService
    {
        private Newtonsoft.Json.JsonSerializerSettings m_minimalSerialiser = new Newtonsoft.Json.JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        };

        /// <summary>Serialises an object into JSON.</summary>
        public string SerialiseObject(object jsonObject)
        {
            if (jsonObject == null)
            {
                return null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, m_minimalSerialiser);
        }

        /// <summary>Deserialises JSON.</summary>
        public JsonType DeserialiseString<JsonType>(string jsonString)
        {
            if (jsonString == null)
            {
                return default(JsonType);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonType>(jsonString);
        }

        /// <summary>Deserialises JSON.</summary>
        public JsonType DeserialiseStream<JsonType>(System.IO.Stream jsonStream)
        {
#if DEBUG
            using (System.IO.TextReader streamReader = new System.IO.StreamReader(jsonStream))
            {
                string jsonString = streamReader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonType>(jsonString);
            }
#else
            using (System.IO.TextReader streamReader = new System.IO.StreamReader(jsonStream))
            {
                using (Newtonsoft.Json.JsonReader jsonReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                {
                    if (m_serialiser == null)
                    {
                        m_serialiser = new Newtonsoft.Json.JsonSerializer();
                    }
                    return m_serialiser.Deserialize<JsonType>(jsonReader);
                }
            }
#endif
        }
    }
}
