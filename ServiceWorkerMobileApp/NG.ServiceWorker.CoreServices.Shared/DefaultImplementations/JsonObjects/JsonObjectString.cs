using System;

namespace NG.ServiceWorker.CoreServices.JsonObjects
{
    public class JsonObjectString : IJsonObject
    {
        #region Base

        /// <summary>Constructor.</summary>
        public JsonObjectString(string value)
        {
            this.AsString = value;
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>The type of the object.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.String; } }

        /// <summary>The type of the object as a string.</summary>
        public string AsString { get; private set; }

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
            return JsonObjectNull.Null;
        }

        #endregion
    }
}
