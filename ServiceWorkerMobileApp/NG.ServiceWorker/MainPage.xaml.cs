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

            // Create view model
            m_viewModel = new MainPageViewModel();
            this.BindingContext = m_viewModel;

            // Run on backgroud thread
            Services.ThreadService.RunActionOnBackgroundThread("FetchJobsOnStartup", () =>
            {
                // Fake data
                ApiModel.Job[] jobArray = Services.ApiService.GetJobArray();

                // Create jobs
                foreach (ApiModel.Job job in jobArray)
                {
                    m_viewModel.JobListViewModels.Add(new Jobs.JobListItemViewModel
                    {
                        Job = job
                    });
                }
            });
        }
    }
}
