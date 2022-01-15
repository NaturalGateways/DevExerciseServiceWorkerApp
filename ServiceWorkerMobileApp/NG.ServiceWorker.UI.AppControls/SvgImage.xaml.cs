using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.ServiceWorker.UI
{
    public partial class SvgImage : ContentView
    {
        /// <summary>A bindable property.</summary>
        public static readonly BindableProperty SvgNameProperty = BindableProperty.Create("SvgName", typeof(string), typeof(SvgImage), defaultBindingMode: BindingMode.TwoWay);
        /// <summary>A bindable property.</summary>
        public string SvgName { get { return (string)GetValue(SvgNameProperty); } set { SetValue(SvgNameProperty, value); } }

        public static readonly BindableProperty RenderColourProperty = BindableProperty.Create(nameof(RenderColour), typeof(Color), typeof(SvgImage), new Color(0.0, 0.0, 0.0, 0.0), BindingMode.OneWay, null, (bindObj, oldvalue, newValue) =>
        {
            SvgImage thisObject = (SvgImage)bindObj;
            thisObject.InvalidateLayout();
        });
        public Color RenderColour
        {
            get { return (Color)GetValue(RenderColourProperty); }
            set { SetValue(RenderColourProperty, value); }
        }

        public uint? RenderColourUint
        {
            get
            {
                Color renderColour = this.RenderColour;
                if (0.0 < renderColour.A)
                {
                    uint redByte = ((uint)(255.0 * renderColour.R)) & 0xFF;
                    uint greenByte = ((uint)(255.0 * renderColour.G)) & 0xFF;
                    uint blueByte = (uint)(255.0 * renderColour.B) & 0xFF;
                    return (redByte) << 16 | (greenByte) << 8 | blueByte;
                }
                return null;
            }
        }

        public SvgImage()
        {
            InitializeComponent();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            // Check svg name
            string svgName = this.SvgName;
            if (string.IsNullOrEmpty(svgName))
            {
                return;
            }

            // Get SVG aspect
            ISvgService svgService = Services.SvgService;
            double svgWoverH = svgService.GetSvgAspectRatioWoverH(svgName);
            double layoutWoverH = width / height;

            // Work out bounds
            double imageWidth = 0.0;
            double imageHeight = 0.0;
            double leftOffset = 0.0;
            double topOffset = 0.0;
            if (svgWoverH < layoutWoverH)
            {
                imageHeight = height;
                imageWidth = height * svgWoverH;
                leftOffset = (width - imageWidth) / 2;
            }
            else
            {
                imageWidth = width;
                imageHeight = width / svgWoverH;
                topOffset = (height - imageHeight) / 2;
            }

            // Get image
            double retinaScale = ServiceWorker.Platform.GetPlatform().RetinaScale;
            ImageSource pngImage = svgService.GetPngFile(svgName, (int)(retinaScale * imageWidth), this.RenderColourUint).AsImageSource;
            this.ImageControl.Source = pngImage;

            // Layout
            LayoutChildIntoBoundingRegion(this.ImageControl, new Rectangle(x + leftOffset, y + topOffset, imageWidth, imageHeight));
        }
    }
}
