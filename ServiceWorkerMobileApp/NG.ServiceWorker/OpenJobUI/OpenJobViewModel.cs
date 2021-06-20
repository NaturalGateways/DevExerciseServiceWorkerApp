using System;

using Xamarin.Forms;

namespace NG.ServiceWorker.OpenJobUI
{
    public class OpenJobViewModel : UI.ViewModel
    {
        public ApiModel.JobWithLinks JobWithLinks { get; set; }

        private bool m_showContactDetail = false;
        public bool ShowContactDetail
        {
            get { return m_showContactDetail; }
            set
            {
                m_showContactDetail = value;
                OnPropertyChanged("IsSummaryContactVisible");
                OnPropertyChanged("IsDetailContactVisible");
            }
        }
        public bool IsSummaryContactVisible { get { return this.ShowContactDetail == false; } }
        public bool IsDetailContactVisible { get { return this.ShowContactDetail; } }

        public string Title { get { return $"Job {this.JobWithLinks.Job.JobKey}"; } }

        public string DisplayName { get { return this.JobWithLinks.Contact.DisplayName; } }

        public string BusinessName { get { return this.JobWithLinks.Contact.BusinessName; } }
        public bool IsBusinessNameVisible { get { return string.IsNullOrEmpty(this.JobWithLinks.Contact.BusinessName) == false; } }

        public string Address { get { return this.JobWithLinks.Contact.Address; } }

        public string Email { get { return this.JobWithLinks.Contact.Email; } }

        public string ProfilePicUrl { get { return this.JobWithLinks.Contact.ProfilePicUrl; } }

        public ImageSource PlaceholderProfilePicImage
        {
            get
            {
                if (this.IsPlaceholderProfilePicVisible)
                {
                    IFile pngFile = Services.SvgService.GetPngFile("icon_profilepic_placeholder", 40, 0xAAAAAA);
                    ImageSource pngImage = pngFile.AsImageSource;
                    return pngImage;
                }
                return null;
            }
        }

        public bool IsProfilePicImageVisible { get { return string.IsNullOrEmpty(this.JobWithLinks.Contact.ProfilePicUrl) == false; } }
        public bool IsPlaceholderProfilePicVisible { get { return string.IsNullOrEmpty(this.JobWithLinks.Contact.ProfilePicUrl); } }

        public string JobStatus { get { return this.JobWithLinks.Job.JobStatusName; } }

        public string JobDescription { get { return this.JobWithLinks.Job.Description; } }

        public string PaymentRate
        {
            get
            {
                if (this.JobWithLinks.Job.PaymentAmount.HasValue == false)
                {
                    return "Unknown";
                }
                string amountCurrency = "$" + this.JobWithLinks.Job.PaymentAmount.Value.ToString("N2");
                if (this.JobWithLinks.Job.PaymentTypeKey == "PerHour")
                {
                    return $"{amountCurrency} per hour";
                }
                return $"{amountCurrency} Quote";
            }
        }

        public string Schedule
        {
            get
            {
                ApiModel.JobScheduleWindow scheduleWindow = this.JobWithLinks.Job.ScheduleWindow;
                if (scheduleWindow == null || (scheduleWindow.StartDateTimeUtc.HasValue == false && scheduleWindow.EndDateTimeUtc.HasValue == false))
                {
                    return "No schedule";
                }
                if (scheduleWindow.StartDateTimeUtc.HasValue == false)
                {
                    string endDateTimeString = scheduleWindow.EndDateTimeUtc.Value.ToLocalTime().ToString();
                    return $"By {endDateTimeString}";
                }
                if (scheduleWindow.EndDateTimeUtc.HasValue == false)
                {
                    string startDateTimeString = scheduleWindow.StartDateTimeUtc.Value.ToLocalTime().ToString();
                    return $"After {startDateTimeString}";
                }
                if (scheduleWindow.StartDateTimeUtc.Value == scheduleWindow.EndDateTimeUtc.Value)
                {
                    string startDateTimeString = scheduleWindow.StartDateTimeUtc.Value.ToLocalTime().ToString();
                    return $"On {startDateTimeString}";
                }
                DateTime localStartDateTime = scheduleWindow.StartDateTimeUtc.Value.ToLocalTime();
                DateTime localEndDateTime = scheduleWindow.EndDateTimeUtc.Value.ToLocalTime();
                if (localStartDateTime.Date == localEndDateTime.Date)
                {
                    string dateString = localStartDateTime.ToShortDateString();
                    string startTimeString = localStartDateTime.ToShortTimeString();
                    string endTimeString = localEndDateTime.ToShortTimeString();
                    return $"{dateString} between {startTimeString} and {endTimeString}";
                }
                {
                    string startDateTimeString = localStartDateTime.ToString();
                    string endDateTimeString = localEndDateTime.ToString();
                    return $"{startDateTimeString} to {endDateTimeString}";
                }
            }
        }

        public string EstimatedTime
        {
            get
            {
                if (this.JobWithLinks.Job.EstimatedTimeMinutes.HasValue == false)
                {
                    return "No estimate";
                }
                int estMins = this.JobWithLinks.Job.EstimatedTimeMinutes.Value;
                if (estMins < 60)
                {
                    if (estMins == 1)
                    {
                        return "1 min";
                    }
                    return $"{estMins} mins";
                }
                int compHours = estMins / 60;
                int compMinutes = estMins % 60;
                if (compMinutes == 0)
                {
                    if (compHours == 1)
                    {
                        return "1 hour";
                    }
                    return $"{compHours} hours";
                }
                if (compHours == 1)
                {
                    return $"1 hour {compMinutes} mins";
                }
                return $"{compHours} hours {compMinutes} mins";
            }
        }
    }
}
