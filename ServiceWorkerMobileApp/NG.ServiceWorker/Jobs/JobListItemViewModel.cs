using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.Jobs
{
    public class JobListItemViewModel : UI.ViewModel
    {
        public Page XamarinView { get; set; }

        public ApiModel.JobWithLinks JobWithLinks { get; set; }

        public string JobTypeName { get { return this.JobWithLinks.Job.Description; } }

        public string CustomerDisplayName { get { return this.JobWithLinks.Contact.DisplayName; } }

        public string Address { get { return this.JobWithLinks.Contact.Address; } }

        public string ProfilePicUrl { get { return this.JobWithLinks.Contact.ProfilePicUrl; } }

        public bool IsProfilePicImageVisible { get { return string.IsNullOrEmpty(this.JobWithLinks.Contact.ProfilePicUrl) == false; } }
        public bool IsPlaceholderProfilePicVisible { get { return string.IsNullOrEmpty(this.JobWithLinks.Contact.ProfilePicUrl); } }

        public ICommand TappedCommand => new Command(OnItemTapped);

        private void OnItemTapped()
        {
            OpenJobUI.OpenJobViewModel jobViewModel = new OpenJobUI.OpenJobViewModel(this.JobWithLinks);
            Page openJobPage = Services.UserInterfaceViewFactoryService.CreatePageFromViewModel<OpenJobUI.OpenJobViewModel>(jobViewModel);
            this.XamarinView.Navigation.PushAsync(openJobPage);
        }
    }
}
