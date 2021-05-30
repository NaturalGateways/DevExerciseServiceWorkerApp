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

        public Xamarin.Forms.ImageSource PlaceholderProfilePicImage
        {
            get
            {
                if (this.IsPlaceholderProfilePicVisible)
                {
                    IFile pngFile = Services.SvgService.GetPngFile("icon_profilepic_placeholder", 40, 0xAAAAAA);
                    Xamarin.Forms.ImageSource pngImage = pngFile.AsImageSource;
                    return pngImage;
                }
                return null;
            }
        }

        public bool IsProfilePicImageVisible { get { return string.IsNullOrEmpty(this.Job.ProfilePicUrl) == false; } }
        public bool IsPlaceholderProfilePicVisible { get { return string.IsNullOrEmpty(this.Job.ProfilePicUrl); } }
    }
}
