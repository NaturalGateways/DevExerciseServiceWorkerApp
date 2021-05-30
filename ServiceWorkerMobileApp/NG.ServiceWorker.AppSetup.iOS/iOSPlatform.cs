using System;

namespace NG.ServiceWorker.AppSetup.iOSSetup
{
    public class iOSPlatform : Platform
    {
        /// <summary>Constructor.</summary>
        public iOSPlatform()
        {
            this.Flags.Add(Platform.PLATFORM_IOS);
        }
    }
}
