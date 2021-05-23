using System;
using System.Reflection;

namespace NG.ServiceWorker.AppSetup
{
    /// <summary>Static function called by each platform app project to do the common startup.</summary>
    public static class CommonAppSetup
    {
        public static void CommonStartup()
        {
            // Read services config
            Assembly servicesDefinitionAssembly = typeof(Services).Assembly;
            Assembly servicesCoreAssembly = typeof(CoreServices.DefaultHttpService).Assembly;
            ConfigReaders.ServiceConfig.ServiceConfigReader.ReadConfig(servicesDefinitionAssembly.GetManifestResourceStream("NG.ServiceWorker.Resources.services_config.xml"), servicesCoreAssembly);
        }
    }
}
