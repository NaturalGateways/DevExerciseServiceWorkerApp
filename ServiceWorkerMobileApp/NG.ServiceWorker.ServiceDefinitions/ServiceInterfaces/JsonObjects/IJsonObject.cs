using System;
using System.Collections.Generic;

namespace NG.ServiceWorker
{
    public enum JsonObjectType
    {
        Null,
        String,
        Array,
        Dictionary
    }

    public interface IJsonObject
    {
        /// <summary>The type of the object.</summary>
        JsonObjectType ObjectType { get; }

        /// <summary>The type of the object as a string.</summary>
        string AsString { get; }
        /// <summary>The type of the object as an array of child objects.</summary>
        IJsonObject[] AsArray { get; }

        /// <summary>Getter for whether the given key exists.</summary>
        bool HasDictionaryKey(string key);
        /// <summary>Getter for the given dictionary object.</summary>
        IJsonObject GetDictionaryObject(string key);
    }
}
