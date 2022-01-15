using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI
{
    public class InvisibleButton : Grid
    {
        #region External XAML bindings

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(InvisibleButton));
        /// <summary>A bindable property.</summary>
        public ICommand Command { get { return (ICommand)GetValue(CommandProperty); } set { SetValue(CommandProperty, value); } }

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty ShowPressedProperty = BindableProperty.Create("ShowPressed", typeof(bool), typeof(InvisibleButton), defaultBindingMode: BindingMode.TwoWay);
        /// <summary>A bindable property.</summary>
        public bool ShowPressed { get { return (bool)GetValue(ShowPressedProperty); } set { SetValue(ShowPressedProperty, value); } }

        #endregion
    }
}
