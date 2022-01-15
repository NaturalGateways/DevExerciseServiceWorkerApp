using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI
{
    public partial class SvgButton : ContentView
    {
        #region External XAML bindings

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty SvgNameProperty = BindableProperty.Create("SvgName", typeof(string), typeof(SvgButton), defaultBindingMode: BindingMode.TwoWay);
        /// <summary>A bindable property.</summary>
        public string SvgName { get { return (string)GetValue(SvgNameProperty); } set { SetValue(SvgNameProperty, value); } }

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty PressedColourProperty = BindableProperty.Create("PressedColour", typeof(Color), typeof(SvgButton), defaultValue: Color.White, propertyChanged: (bindObj, oldVal, newVal) =>
        {
            ((SvgButton)bindObj).OnPropertyChanged("InternalIconColour");
        });
        /// <summary>A bindable property.</summary>
        public Color PressedColour { get { return (Color)GetValue(PressedColourProperty); } set { SetValue(PressedColourProperty, value); } }

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty RenderColourProperty = BindableProperty.Create("RenderColour", typeof(Color), typeof(SvgButton), defaultValue: Color.Black, propertyChanged: (bindObj, oldVal, newVal) =>
        {
            ((SvgButton)bindObj).OnPropertyChanged("InternalIconColour");
        });
        /// <summary>A bindable property.</summary>
        public Color RenderColour { get { return (Color)GetValue(RenderColourProperty); } set { SetValue(RenderColourProperty, value); } }

        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(SvgButton), defaultBindingMode: BindingMode.TwoWay);
        /// <summary>A bindable property.</summary>
        public ICommand Command { get { return (ICommand)GetValue(CommandProperty); } set { SetValue(CommandProperty, value); } }

        #endregion

        #region Base

        /// <summary>Constructor.</summary>
        public SvgButton()
        {
            InitializeComponent();

            this.BindingTarget.BindingContext = this;
        }

        #endregion

        #region Internal XAML bindings

        /// <summary>A bindable property.</summary>
        private bool m_pressed = false;
        /// <summary>A bindable property.</summary>
        public bool Pressed
        {
            get { return m_pressed; }
            set
            {
                m_pressed = value;
                OnPropertyChanged("InternalIconColour");
            }
        }

        /// <summary>A bindable property.</summary>
        public Color InternalIconColour
        {
            get
            {
                if (this.Pressed)
                {
                    const double pressedAlpha = 0.5;
                    const double inverseAlpha = 1.0 - pressedAlpha;
                    Color backColour = this.PressedColour;
                    Color foreColour = this.RenderColour;
                    return new Color(inverseAlpha * backColour.R + pressedAlpha * foreColour.R,
                                     inverseAlpha * backColour.G + pressedAlpha * foreColour.G,
                                     inverseAlpha * backColour.B + pressedAlpha * foreColour.B);
                }
                return this.RenderColour;
            }
        }

        #endregion
    }
}
