using System;

using UIKit;

namespace NG.ServiceWorker.AppSetup.iOSSetup
{
    public class iOSPlatform : Platform
    {
        /// <summary>Constructor.</summary>
        public iOSPlatform()
        {
            this.Flags.Add(Platform.PLATFORM_IOS);
        }

        /// <summary>iOS has a retina scale.</summary>
        public override double RetinaScale { get { return UIScreen.MainScreen.Scale; } }
    }
}
