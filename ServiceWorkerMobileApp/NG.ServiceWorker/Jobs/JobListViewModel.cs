using System;
using System.Collections.ObjectModel;

namespace NG.ServiceWorker.Jobs
{
    public class JobListViewModel
    {
        public ObservableCollection<JobListItemViewModel> JobListViewModels { get; private set; } = new ObservableCollection<JobListItemViewModel>();
    }
}
