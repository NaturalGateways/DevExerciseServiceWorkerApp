using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.ServiceWorker.CoreServices
{
    public class DefaultSvgService : ISvgService
    {
        #region Base

        /// <summary>The confgiured SVG files.</summary>
        private Dictionary<string, AppImages.Config.SvgFile> m_svgFilesById = new Dictionary<string, AppImages.Config.SvgFile>();

        /// <summary>The confgiured SVG files.</summary>
        private Dictionary<string, List<RenderedPng>> m_pngListsById = new Dictionary<string, List<RenderedPng>>();

        /// <summary>Constructor.</summary>
        public DefaultSvgService()
        {
            // Read config
            m_svgFilesById = AppImages.Config.ConfigReader.ReadConfig().ToDictionary(x => x.SvgId);
        }

        #endregion

        #region Rendered PNG class

        /// <summary>The class for a rendered PNG.</summary>
        private class RenderedPng
        {
            public string SvgId { get; set; }

            public int Width { get; set; }

            public uint? ColourRGB { get; set; }

            public IFile PngFile { get; set; }
        }

        #endregion

        #region SVG to PNG Writer

        /// <summary>Writer for writing an SVG file to a PNG file.</summary>
        private class SvgToPngWriter : IFileWriter
        {
            #region IFileWriter implementation

            /// <summary>The resource ID.</summary>
            private AppImages.Config.SvgFile m_svgConfig = null;
            /// <summary>The resource ID.</summary>
            private int m_imageWidth = 0;
            /// <summary>The resource ID.</summary>
            private uint? m_colourRGB = null;

            /// <summary>Constructor.</summary>
            public SvgToPngWriter(AppImages.Config.SvgFile svgConfig, int imageWidth, uint? colourRGB)
            {
                m_svgConfig = svgConfig;
                m_imageWidth = imageWidth;
                m_colourRGB = colourRGB;
            }

            #endregion

            #region IFileWriter implementation

            /// <summary>The extension of the file.</summary>
            public string FileDataExtension { get { return "png"; } }

            /// <summary>Called to write the data to a created file.</summary>
            public void WriteDataToStream(System.IO.Stream stream)
            {
                AppImages.SvgToPngRenderer.RenderPng(stream, m_svgConfig, m_imageWidth, m_colourRGB);
            }

            #endregion
        }

        #endregion

        #region ISvgService implementation

        /// <summary>Gets a PNG file for an SVG resource.</summary>
        public IFile GetPngFile(string svgId, int imageWidth, uint? colourRGB)
        {
            lock (m_pngListsById)
            {
                // Check cache
                if (m_pngListsById.ContainsKey(svgId))
                {
                    RenderedPng existingPng = m_pngListsById[svgId].FirstOrDefault(x => x.Width == imageWidth && x.ColourRGB == colourRGB);
                    if (existingPng != null)
                    {
                        return existingPng.PngFile;
                    }
                }

                // Get SVG config
                if (m_svgFilesById.ContainsKey(svgId) == false)
                {
                    throw new Exception($"SVG file '{svgId}' does not exist in config.");
                }
                AppImages.Config.SvgFile svgConfig = m_svgFilesById[svgId];

                // Create file
                RenderedPng renderedPng = new RenderedPng
                {
                    SvgId = svgId,
                    Width = imageWidth,
                    ColourRGB = colourRGB,
                    PngFile = Services.FileService.CreateTempFile(new SvgToPngWriter(svgConfig, imageWidth, colourRGB))
                };
                if (m_pngListsById.ContainsKey(svgId))
                {
                    m_pngListsById[svgId].Add(renderedPng);
                }
                else
                {
                    m_pngListsById[svgId] = new List<RenderedPng> { renderedPng };
                }
                return renderedPng.PngFile;
            }
        }

        #endregion
    }
}
