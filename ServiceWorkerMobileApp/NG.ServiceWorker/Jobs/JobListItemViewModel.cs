using System;

namespace NG.ServiceWorker.Jobs
{
    public class JobListItemViewModel : UI.ViewModel
    {
        public string JobTypeName { get; set; }

        public string CustomerDisplayName { get; set; }

        public string Address { get; set; }

        public string ProfilePicUrl { get; set; }
    }
}
