using System;

namespace NG.ServiceWorker.Jobs
{
    public class JobListItemViewModel : UI.ViewModel
    {
        public ApiModel.Job Job { get; set; }

        public string JobTypeName { get { return this.Job.JobTypeName; } }

        public string CustomerDisplayName { get { return this.Job.CustomerDisplayName; } }

        public string Address { get { return this.Job.Address; } }

        public string ProfilePicUrl { get { return this.Job.ProfilePicUrl; } }
    }
}
