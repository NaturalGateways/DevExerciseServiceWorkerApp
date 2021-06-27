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

        /// <summary>iOS has a retina scale.</summary>
        public override double RetinaScale { get { return Android.App.Application.Context.Resources.DisplayMetrics.ScaledDensity; } }
    }
}
