using System;

namespace NG.ServiceWorker.AppSetup.WindowsSetup
{
    public class WindowsPlatform : Platform
    {
        /// <summary>Constructor.</summary>
        public WindowsPlatform()
        {
            this.Flags.Add(Platform.PLATFORM_WINDOWS);
            this.Flags.Add(Platform.PLATFORM_UWP);
        }
    }
}
