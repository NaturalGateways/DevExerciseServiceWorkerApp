using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NG.ServiceWorker
{
    public partial class MainPage : TabbedPage
    {
        private MainPageViewModel m_viewModel = null;

        public MainPage()
        {
            InitializeComponent();

            // Create view model
            m_viewModel = new MainPageViewModel();

#if DEBUG
            // Add debug menu
            DebugUI.DebugPage debugPage = new DebugUI.DebugPage();
            NavigationPage debugNavPage = new NavigationPage(debugPage);
            debugNavPage.Title = "Debug";
            debugNavPage.IconImageSource = "icon_maintabs_system";
            this.Children.Add(debugNavPage);
#endif

            // Set view model
            this.BindingContext = m_viewModel;
        }
    }
}
