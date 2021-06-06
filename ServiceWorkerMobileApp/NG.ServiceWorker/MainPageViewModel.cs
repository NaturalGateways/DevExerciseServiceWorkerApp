using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace NG.ServiceWorker
{
    public class MainPageViewModel : UI.ViewModel
    {
        public ImageSource JobsTabIconImage { get { return Services.SvgService.GetPngFile("icon_maintabs_jobs", 30, null).AsImageSource; } }

        public ImageSource MapTabIconImage { get { return Services.SvgService.GetPngFile("icon_maintabs_map", 30, null).AsImageSource; } }

        public ImageSource SystemTabIconImage { get { return Services.SvgService.GetPngFile("icon_maintabs_system", 30, null).AsImageSource; } }
    }
}
