using System;
using System.Collections.Generic;

namespace NG.ServiceWorker.Images.Config
{
    public static class ConfigReader
    {
        public static IEnumerable<SvgFile> ReadConfig()
        {
            List<SvgFile> svgFileList = new List<SvgFile>();

            // Read config
            System.IO.Stream xmlStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("NG.ServiceWorker.Images.svg-files.xml");
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(xmlStream))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name == "svg")
                    {
                        svgFileList.Add(ReadSvgTag(reader));
                    }
                }
            }

            // return
            return svgFileList;
        }

        private static SvgFile ReadSvgTag(System.Xml.XmlReader reader)
        {
            string svgId = reader.GetAttribute("id");
            string resId = reader.GetAttribute("resId");
            double width = Double.Parse(reader.GetAttribute("width"));
            double height = Double.Parse(reader.GetAttribute("height"));
            return new SvgFile
            {
                SvgId = svgId,
                ResId = resId,
                AspectRatioWoverH = width / height
            };
        }
    }
}
