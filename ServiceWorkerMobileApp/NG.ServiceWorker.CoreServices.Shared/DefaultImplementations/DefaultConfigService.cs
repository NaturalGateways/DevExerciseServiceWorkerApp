using System;
using System.Reflection;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultConfigService : IConfigService
    {
        #region Base

        /// <summary>The release config JSON.</summary>
        private IJsonObject m_releaseConfigJson = null;

        /// <summary>Constructor.</summary>
        public DefaultConfigService()
        {
            // Load the release config
            Assembly assembly = typeof(IConfigService).Assembly;
            System.IO.Stream releaseConfigStream = assembly.GetManifestResourceStream("NG.ServiceWorker.Resources.release_config.json");
            object releaseConfigObject = Services.JsonService.DeserialiseStream<object>(releaseConfigStream);
            m_releaseConfigJson = Services.JsonService.JsonObjectFromAnonObject(releaseConfigObject);
        }

        #endregion

        #region IConfigService implementation

        /// <summary>Gets the string with the given key.</summary>
        public string GetString(string key)
        {
            // Check release config
            {
                IJsonObject valueJson = m_releaseConfigJson.GetDictionaryObject(key);
                if (valueJson.ObjectType == JsonObjectType.String)
                {
                    return valueJson.AsString;
                }
            }

            // We didn't find it
            return null;
        }

        #endregion
    }
}
