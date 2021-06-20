using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NG.ServiceWorker.OpenJobUI
{
    public partial class OpenJobPage : ContentPage
    {
        private const int MIN_CONTACT_HEIGHT = 80;
        private const int DETAIL_TOTALVMARGIN = 4 + 4;

        public OpenJobPage(OpenJobViewModel viewModel)
        {
            InitializeComponent();

            this.ContactHeaderGrid.HeightRequest = MIN_CONTACT_HEIGHT;

            if (viewModel.JobWithLinks.Contact.PhoneNumbers != null)
            {
                foreach (string phoneNumber in viewModel.JobWithLinks.Contact.PhoneNumbers)
                {
                    this.PhoneNumberStack.Children.Add(new Label
                    {
                        Text = phoneNumber,
                        Style = (Style)this.Resources["ContactLabel"]
                    });
                }
            }
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OpenJobViewModel viewModel = this.BindingContext as OpenJobViewModel;
            if (viewModel != null)
            {
                if (viewModel.ShowContactDetail)
                {
                    Animation animation = new Animation(x => this.ContactHeaderGrid.HeightRequest = x, this.ContactHeaderGrid.HeightRequest, MIN_CONTACT_HEIGHT, Easing.SinOut);
                    animation.Commit(this.ContactHeaderGrid, "HideContactDetail", 16, 200);
                    viewModel.ShowContactDetail = false;
                }
                else
                {
                    // Calculate new height
                    SizeRequest measurement = this.ContactDetailStack.Measure(this.Width, this.Height);
                    double contactHeight = Math.Max(MIN_CONTACT_HEIGHT, measurement.Request.Height + DETAIL_TOTALVMARGIN);

                    // Animate
                    Animation animation = new Animation(x => this.ContactHeaderGrid.HeightRequest = x, this.ContactHeaderGrid.HeightRequest, contactHeight, Easing.SinOut);
                    animation.Commit(this.ContactHeaderGrid, "ShowContactDetail", 16, 200);
                    viewModel.ShowContactDetail = true;
                }
            }
        }
    }
}
