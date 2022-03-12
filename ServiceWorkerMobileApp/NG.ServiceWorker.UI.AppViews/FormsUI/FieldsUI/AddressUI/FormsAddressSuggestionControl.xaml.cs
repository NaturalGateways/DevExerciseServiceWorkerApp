using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.FormsUI.FieldsUI.AddressUI
{
    public partial class FormsAddressSuggestionControl : ContentView
    {
        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty AddressProperty = BindableProperty.Create("Address", typeof(string), typeof(FormsAddressSuggestionControl), null, BindingMode.TwoWay);
        /// <summary>A bindable property.</summary>
        public string Address { get { return (string)GetValue(AddressProperty); } set { SetValue(AddressProperty, value); } }

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create("ClickCommand", typeof(ICommand), typeof(FormsAddressSuggestionControl), null, BindingMode.TwoWay);
        /// <summary>A bindable property.</summary>
        public ICommand ClickCommand { get { return (ICommand)GetValue(ClickCommandProperty); } set { SetValue(ClickCommandProperty, value); } }

        public FormsAddressSuggestionControl()
        {
            InitializeComponent();

            this.BindingTarget.BindingContext = this;
        }

        /// <summary>Pressed property.</summary>
        private bool m_pressed = false;
        /// <summary>Pressed property.</summary>
        public bool Pressed { get { return m_pressed; } set { m_pressed = value; OnPropertyChanged("Pressed"); } }
    }
}
