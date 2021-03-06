using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace NG.ServiceWorker.Droid
{
    [Activity(Label = "NG.ServiceWorker", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static AppSetup.AndroidSetup.AndroidPlatform s_platformSingleton = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Startup or resume
            if (s_platformSingleton == null)
            {
                s_platformSingleton = AppSetup.AndroidSetup.AndroidAppStartup.OnStartup(this);
            }
            else
            {
                AppSetup.AndroidSetup.AndroidAppStartup.OnResume(s_platformSingleton, this);
            }

            // Load application
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}