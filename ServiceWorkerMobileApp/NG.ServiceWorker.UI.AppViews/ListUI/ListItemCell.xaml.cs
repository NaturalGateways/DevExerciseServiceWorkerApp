using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NG.ServiceWorker.UI.ListUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListItemCell : ViewCell
    {
        public ListItemCell()
        {
            InitializeComponent();

            // For UWP, the height must be set manually to a constant
            if (ServiceWorker.Platform.GetPlatform().HasFlag(ServiceWorker.Platform.PLATFORM_UWP))
            {
                this.BackgroundGrid.HeightRequest = 60;
            }
        }
    }
}
