using System;

using Android.Content;

namespace NG.ServiceWorker.AppSetup.AndroidSetup
{
    public static class AndroidAppStartup
    {
        public static AndroidPlatform OnStartup(Context context)
        {
            AndroidPlatform platform = new AndroidPlatform(context);
            CommonAppSetup.CommonStartup();
            return platform;
        }

        public static void OnResume(AndroidPlatform platform, Context context)
        {
            // Set context on platform
            platform.Context = context;
        }
    }
}
