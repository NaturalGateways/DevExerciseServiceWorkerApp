using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NG.ServiceWorker
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel m_viewModel = null;

        public MainPage()
        {
            InitializeComponent();

            // Create jobs
            m_viewModel = new MainPageViewModel();
            for (int i = 0; i != 1; ++i)
            {
                m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                {
                    JobTypeName = "Cage Clean Out",
                    CustomerDisplayName = "Ragnar",
                    Address = "Katee Habitat cage",
                    ProfilePicUrl = "http://files.naturalgateways.com/Profiles/Ragnar.png"
                });
                m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                {
                    JobTypeName = "Bath",
                    CustomerDisplayName = "Flynn",
                    Address = "Katee Habitat cage",
                    ProfilePicUrl = "http://files.naturalgateways.com/Profiles/Flynn.png"
                });
                m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                {
                    JobTypeName = "Transport to pet show",
                    CustomerDisplayName = "Myron",
                    Address = "Back of DVD case",
                    ProfilePicUrl = "http://files.naturalgateways.com/Profiles/Myron.png"
                });
                m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                {
                    JobTypeName = "Transport to pet show",
                    CustomerDisplayName = "Mixon",
                    Address = "Recyclables pile",
                    ProfilePicUrl = "http://files.naturalgateways.com/Profiles/Mixon.png"
                });
                m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                {
                    JobTypeName = "Food delivery",
                    CustomerDisplayName = "Biff",
                    Address = "Bird box",
                    ProfilePicUrl = "http://files.naturalgateways.com/Profiles/Biff.png"
                });
                m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                {
                    JobTypeName = "Playtime",
                    CustomerDisplayName = "Marty",
                    Address = "Under the drop sheet",
                    ProfilePicUrl = "http://files.naturalgateways.com/Profiles/Marty.png"
                });
            }
            this.BindingContext = m_viewModel;
        }
    }
}
