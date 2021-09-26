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
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FieldsUI.FormsDateTimeFieldViewModel, UI.FormsUI.FieldsUI.FormsDateTimeFieldView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FieldsUI.FormsReadOnlyFieldViewModel, UI.FormsUI.FieldsUI.FormsReadOnlyFieldView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FieldsUI.FormsSegueSelectionFieldViewModel, UI.FormsUI.FieldsUI.FormsSegueSelectionFieldView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FieldsUI.FormsTextFieldViewModel, UI.FormsUI.FieldsUI.FormsTextFieldView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.FieldsUI.FormsToggleButtonSelectionFieldViewModel, UI.FormsUI.FieldsUI.FormsToggleButtonSelectionFieldView>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.InputPageUI.SegueMultiSelectionInputPageViewModel, UI.FormsUI.InputPageUI.SegueMultiSelectionInputPage>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<UI.FormsUI.InputPageUI.SegueSelectionInputPageViewModel, UI.FormsUI.InputPageUI.SegueSelectionInputPage>();
            Services.UserInterfaceViewFactoryService.RegisterViewModelViewMapping<OpenJobUI.OpenJobViewModel, OpenJobUI.OpenJobPage>();

            // Start sync
            Services.SyncService.StartSync();
        }
    }
}
