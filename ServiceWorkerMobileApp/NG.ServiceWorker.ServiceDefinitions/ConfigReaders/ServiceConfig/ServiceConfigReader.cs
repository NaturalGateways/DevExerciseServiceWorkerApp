using System;
using System.Collections.Generic;
using System.Reflection;

namespace NG.ServiceWorker.ConfigReaders.ServiceConfig
{
    public static class ServiceConfigReader
    {
        public class ServiceMapping
        {
            public string InterfaceTypeString { get; set; }

            public string ImplementationTypeString { get; set; }
        }

        public static void ReadConfig(System.IO.Stream xmlStream, Assembly coreAssembly)
        {
            Dictionary<string, ServiceMapping> servicesMappingsByInterfaceType = new Dictionary<string, ServiceMapping>();

            // Read config
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(xmlStream))
            {
                ServiceMapping curService = null;

                while (reader.Read())
                {
                    if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name == "service")
                    {
                        if (reader.IsEmptyElement)
                        {
                            throw new Exception("<service> tag cannot be empty.");
                        }
                        curService = ReadServiceTag(reader);
                        servicesMappingsByInterfaceType[curService.InterfaceTypeString] = curService;
                    }
                    else if (curService != null && reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name == "implementation")
                    {
                        ReadServiceTag(reader, curService);
                    }
                    else if (reader.NodeType == System.Xml.XmlNodeType.EndElement && reader.Name == "service")
                    {
                        curService = null;
                    }
                }
            }

            // Apply config
            {
                Assembly defAssembly = Assembly.GetExecutingAssembly();
                foreach (ServiceMapping serviceMapping in servicesMappingsByInterfaceType.Values)
                {
                    if (string.IsNullOrEmpty(serviceMapping.ImplementationTypeString))
                    {
                        throw new Exception($"Service '{serviceMapping.InterfaceTypeString}' has no implementation.");
                    }
                    Type interfaceType = defAssembly.GetType(serviceMapping.InterfaceTypeString);
                    Type implementationType = coreAssembly.GetType(serviceMapping.ImplementationTypeString);
                    if (implementationType == null)
                    {
                        throw new Exception($"Service implementation '{serviceMapping.ImplementationTypeString}' doesn't exist.");
                    }
                    ServiceProvider.RegisterService(interfaceType, implementationType);
                }
            }
        }

        private static ServiceMapping ReadServiceTag(System.Xml.XmlReader reader)
        {
            string interfaceType = reader.GetAttribute("type");
            return new ServiceMapping
            {
                InterfaceTypeString = interfaceType
            };
        }

        private static void ReadServiceTag(System.Xml.XmlReader reader, ServiceMapping curService)
        {
            string typeString = reader.GetAttribute("type");
            string flagString = reader.GetAttribute("flag");

            if (string.IsNullOrEmpty(flagString) == false && Platform.GetPlatform().HasFlag(flagString) == false)
            {
                return;
            }

            curService.ImplementationTypeString = typeString;
        }
    }
}
