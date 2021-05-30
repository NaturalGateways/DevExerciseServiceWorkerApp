using System;

namespace NG.ServiceWorker
{
    public interface ISvgService
    {
        /// <summary>Gets a PNG file for an SVG resource.</summary>
        IFile GetPngFile(string svgId, int imageWidth, uint? colourRGB);
    }
}
