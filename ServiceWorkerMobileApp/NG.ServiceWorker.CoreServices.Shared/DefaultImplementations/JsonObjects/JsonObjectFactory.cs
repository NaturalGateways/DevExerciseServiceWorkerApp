using System;

namespace NG.ServiceWorker.CoreServices.JsonObjects
{
    public static class JsonObjectFactory
    {
        /// <summary>Creates from an object.</summary>
        public static IJsonObject FromToken(Newtonsoft.Json.Linq.JToken jtoken)
        {
#if DEBUG
            string tokenType = jtoken.GetType().FullName;
#endif
            Newtonsoft.Json.Linq.JObject jObject = jtoken as Newtonsoft.Json.Linq.JObject;
            if (jObject != null)
            {
                return new JsonObjectDictionary(jObject);
            }
            Newtonsoft.Json.Linq.JArray jArray = jtoken as Newtonsoft.Json.Linq.JArray;
            if (jArray != null)
            {
                return new JsonObjectArray(jArray);
            }
            Newtonsoft.Json.Linq.JValue jValue = jtoken as Newtonsoft.Json.Linq.JValue;
            if (jValue != null)
            {
                return new JsonObjectString(jValue.ToString());
            }
            return JsonObjectNull.Null;
        }

        /// <summary>Creates from an object.</summary>
        public static IJsonObject FromObject(object anonObject)
        {
#if DEBUG
            string objectType = anonObject.GetType().FullName;
#endif
            Newtonsoft.Json.Linq.JToken jToken = anonObject as Newtonsoft.Json.Linq.JToken;
            if (jToken != null)
            {
                return FromToken(jToken);
            }
            string stringValue = anonObject as string;
            if (stringValue != null)
            {
                return new JsonObjectString(stringValue);
            }
            return JsonObjectNull.Null;
        }
    }
}
