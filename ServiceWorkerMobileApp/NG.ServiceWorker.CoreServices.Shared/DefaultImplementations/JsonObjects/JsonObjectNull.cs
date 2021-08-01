using System;

namespace NG.ServiceWorker.CoreServices.JsonObjects
{
    public class JsonObjectNull : IJsonObject
    {
        #region Base

        /// <summary>Singleton null object.</summary>
        private static IJsonObject s_singleton = null;
        /// <summary>Singleton null object.</summary>
        public static IJsonObject Null
        {
            get
            {
                if (s_singleton == null)
                    s_singleton = new JsonObjectNull();
                return s_singleton;
            }
        }

        /// <summary>Constructor.</summary>
        private JsonObjectNull()
        {
            //
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>The type of the object.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.Null; } }

        /// <summary>The type of the object as a string.</summary>
        public string AsString { get { return null; } }

        /// <summary>The type of the object as an array of child objects.</summary>
        public IJsonObject[] AsArray { get { return Array.Empty<IJsonObject>(); } }

        /// <summary>Getter for whether the given key exists.</summary>
        public bool HasDictionaryKey(string key)
        {
            return false;
        }

        /// <summary>Getter for the given dictionary object.</summary>
        public IJsonObject GetDictionaryObject(string key)
        {
            return Null;
        }

        #endregion
    }
}
