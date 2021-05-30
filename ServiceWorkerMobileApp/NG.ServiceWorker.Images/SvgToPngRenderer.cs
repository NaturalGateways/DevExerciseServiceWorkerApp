using System;
using System.Reflection;

using SkiaSharp;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace NG.ServiceWorker.Images
{
    public static class SvgToPngRenderer
    {
        public static void RenderPng(System.IO.Stream pngStream, Config.SvgFile svgConfig, int imageWidth, uint? colourRGB)
        {
            // Calculate height from aspect ratio
            int imageHeight = (int)(imageWidth / svgConfig.AspectRatioWoverH);

            // Load SVG
            SKSvg svg = new SKSvg(new SKSize(imageWidth, imageHeight));
            System.IO.Stream svgStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(svgConfig.ResId);
            svg.Load(svgStream);
            if (svg.Picture == null)
            {
                throw new Exception("SVG does not have a Picture after being loaded.");
            }

            // Draw to bitmap
            SKBitmap bitmap = new SKBitmap(imageWidth, imageHeight);
            {
                SKCanvas canvas = new SKCanvas(bitmap);
                canvas.Clear(SKColors.Transparent);
                if (colourRGB.HasValue)
                {
                    using (SKPaint paint = new SKPaint())
                    {
                        paint.ColorFilter = SKColorFilter.CreateBlendMode(new SKColor((byte)((colourRGB.Value >> 16) & 0xFF), (byte)((colourRGB.Value >> 8) & 0xFF), (byte)(colourRGB.Value & 0xFF), 255), SKBlendMode.SrcIn);
                        canvas.DrawPicture(svg.Picture, paint);
                    }
                }
                else
                {
                    canvas.DrawPicture(svg.Picture);
                }
                canvas.Flush();
                canvas.Save();
            }

            // Write rendered image
            using (SKImage image = SKImage.FromBitmap(bitmap))
            {
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 90))
                {
                    data.SaveTo(pngStream);
                }
            }
        }
    }
}
