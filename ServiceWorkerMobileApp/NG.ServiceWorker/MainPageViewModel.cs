using System;
using System.Collections.ObjectModel;

namespace NG.ServiceWorker
{
    public class MainPageViewModel : UI.ViewModel
    {
        public ObservableCollection<Jobs.JobListItemViewModel> JobListViewModels { get; private set; } = new ObservableCollection<Jobs.JobListItemViewModel>();
    }
}
