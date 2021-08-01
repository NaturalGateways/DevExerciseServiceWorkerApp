using System;
using System.Linq;

namespace NG.ServiceWorker.CoreServices.JsonObjects
{
    public class JsonObjectArray : IJsonObject
    {
        #region Base

        /// <summary>The JSON object.</summary>
        private Newtonsoft.Json.Linq.JArray m_jArray = null;

        /// <summary>Constructor.</summary>
        public JsonObjectArray(Newtonsoft.Json.Linq.JArray jArray)
        {
            m_jArray = jArray;
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>The type of the object.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.Array; } }

        /// <summary>The type of the object as a string.</summary>
        public string AsString { get { return null; } }

        /// <summary>The type of the object as an array of child objects.</summary>
        public IJsonObject[] AsArray
        {
            get
            {
                return m_jArray.Select(x => JsonObjectFactory.FromToken(x)).ToArray();
            }
        }

        /// <summary>Getter for whether the given key exists.</summary>
        public bool HasDictionaryKey(string key)
        {
            return false;
        }

        /// <summary>Getter for the given dictionary object.</summary>
        public IJsonObject GetDictionaryObject(string key)
        {
            return JsonObjectNull.Null;
        }

        #endregion
    }
}
