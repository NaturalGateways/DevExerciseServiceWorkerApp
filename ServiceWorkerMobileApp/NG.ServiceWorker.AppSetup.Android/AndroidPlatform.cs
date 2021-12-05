using System;

using Android.Content;

namespace NG.ServiceWorker.AppSetup.AndroidSetup
{
    public class AndroidPlatform : Platform
    {
        #region Base

        /// <summary>The current UI context.</summary>
        public Context Context { get; set; }

        /// <summary>Constructor.</summary>
        public AndroidPlatform(Context context)
        {
            this.Flags.Add(Platform.PLATFORM_ANDROID);

            this.Context = context;
        }

        #endregion

        /// <summary>iOS has a retina scale.</summary>
        public override double RetinaScale { get { return Android.App.Application.Context.Resources.DisplayMetrics.ScaledDensity; } }

        /// <summary>Getter for an OS object.</summary>
        public override object GetOsObject(string objectName)
        {
            switch (objectName)
            {
                case OS_ANDROID_CONTEXT:
                    return this.Context;
            }
            return base.GetOsObject(objectName);
        }
    }
}
