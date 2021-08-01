using System;

namespace NG.ServiceWorker.CoreServices.JsonObjects
{
    public class JsonObjectDictionary : IJsonObject
    {
        #region Base

        /// <summary>The JSON object.</summary>
        private Newtonsoft.Json.Linq.JObject m_jObject = null;

        /// <summary>Constructor.</summary>
        public JsonObjectDictionary(Newtonsoft.Json.Linq.JObject jObject)
        {
            m_jObject = jObject;
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>The type of the object.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.Dictionary; } }

        /// <summary>The type of the object as a string.</summary>
        public string AsString { get { return null; } }

        /// <summary>The type of the object as an array of child objects.</summary>
        public IJsonObject[] AsArray { get { return Array.Empty<IJsonObject>(); } }

        /// <summary>Getter for whether the given key exists.</summary>
        public bool HasDictionaryKey(string key)
        {
            return m_jObject.ContainsKey(key);
        }

        /// <summary>Getter for the given dictionary object.</summary>
        public IJsonObject GetDictionaryObject(string key)
        {
            if (m_jObject.ContainsKey(key))
            {
                return JsonObjectFactory.FromObject(m_jObject[key]);
            }
            return JsonObjectNull.Null;
        }

        #endregion
    }
}
