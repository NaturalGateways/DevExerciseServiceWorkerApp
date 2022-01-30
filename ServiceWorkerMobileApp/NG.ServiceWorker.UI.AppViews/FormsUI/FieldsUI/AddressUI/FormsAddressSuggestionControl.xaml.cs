using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI.AddressUI
{
    public class FormsAddressSuggestionViewModel : ViewModel
    {
        /// <summary>Pressed property.</summary>
        private bool m_pressed = false;
        /// <summary>Pressed property.</summary>
        public bool Pressed { get { return m_pressed; } set { m_pressed = value; OnPropertyChanged("Pressed"); } }
    }

    public partial class FormsAddressSuggestionControl : ContentView
    {
        public FormsAddressSuggestionControl()
        {
            InitializeComponent();

            this.BindingContext = new FormsAddressSuggestionViewModel();
        }
    }
}
