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

            // Register UI mappings
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.ListUI.ListViewModel, UI.ListUI.ListView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FormsDocumentViewModel, UI.FormsUI.FormsDocumentView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FormsSectionViewModel, UI.FormsUI.FormsSectionView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FormsFieldReadOnlyViewModel, UI.FormsUI.FormsFieldReadOnlyView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<OpenJobUI.OpenJobViewModel, OpenJobUI.OpenJobPage>();
        }
    }
}
