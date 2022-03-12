using System;

namespace NG.ServiceWorker
{
    public interface IConfigService
    {
        /// <summary>Gets the string with the given key.</summary>
        string GetString(string key);
    }
}
