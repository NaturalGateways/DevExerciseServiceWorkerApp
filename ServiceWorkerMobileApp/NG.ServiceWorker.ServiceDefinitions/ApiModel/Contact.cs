using System;

namespace NG.ServiceWorker.ApiModel
{
    public class Contact
    {
        public string ProfilePicUrl { get; set; }

        public string DisplayName { get; set; }

        public string Title { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string BusinessName { get; set; }

        public string Address { get; set; }

        public string[] PhoneNumbers { get; set; }

        public string Email { get; set; }
    }
}
