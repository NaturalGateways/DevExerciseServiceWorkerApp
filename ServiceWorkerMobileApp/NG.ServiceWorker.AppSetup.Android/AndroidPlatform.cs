using System;

namespace NG.ServiceWorker.AppSetup.AndroidSetup
{
    public class AndroidPlatform : Platform
    {
        /// <summary>Constructor.</summary>
        public AndroidPlatform()
        {
            this.Flags.Add(Platform.PLATFORM_ANDROID);
        }
    }
}
