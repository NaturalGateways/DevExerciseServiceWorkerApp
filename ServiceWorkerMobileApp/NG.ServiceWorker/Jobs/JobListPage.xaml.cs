using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NG.ServiceWorker.Jobs
{
    public partial class JobListPage : ContentPage
    {
        private JobListViewModel m_viewModel = null;

        public JobListPage()
        {
            InitializeComponent();

            // Create view model
            m_viewModel = new JobListViewModel();
            this.BindingContext = m_viewModel;

            // Run on backgroud thread
            Services.ThreadService.RunActionOnBackgroundThread("FetchJobsOnStartup", () =>
            {
                // Fake data
                ApiModel.Job[] jobArray = Services.ApiService.GetJobArray();

                // Create jobs
                foreach (ApiModel.Job job in jobArray)
                {
                    m_viewModel.JobListViewModels.Add(new JobListItemViewModel
                    {
                        Job = job
                    });
                }
            });
        }
    }
}
