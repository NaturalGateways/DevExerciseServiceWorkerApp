using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NG.ServiceWorker.DebugUI.FilesystemDebugUI
{
    public partial class DebugfilesystemImagePage : ContentPage
    {
        public DebugfilesystemImagePage(string filepath)
        {
            InitializeComponent();

            // Set title
            string filename = System.IO.Path.GetFileName(filepath);
            this.Title = filename;

            // Set image
            this.ImageView.Source = ImageSource.FromFile(filepath);
        }
    }
}
