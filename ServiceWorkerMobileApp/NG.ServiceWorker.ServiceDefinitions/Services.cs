using System;

namespace NG.ServiceWorker
{
    public static class Services
    {
        public static IApiService ApiService { get { return ServiceProvider.GetService<IApiService>(); } }

        public static IFileService FileService { get { return ServiceProvider.GetService<IFileService>(); } }

        public static IFileSystemService FileSystemService { get { return ServiceProvider.GetService<IFileSystemService>(); } }

        public static IFormsService FormsService { get { return ServiceProvider.GetService<IFormsService>(); } }

        public static IHttpService HttpService { get { return ServiceProvider.GetService<IHttpService>(); } }

        public static IJsonService JsonService { get { return ServiceProvider.GetService<IJsonService>(); } }

        public static ILogService LogService { get { return ServiceProvider.GetService<ILogService>(); } }

        public static IMainDataService MainDataService { get { return ServiceProvider.GetService<IMainDataService>(); } }

        public static ISqliteService SqliteService { get { return ServiceProvider.GetService<ISqliteService>(); } }

        public static ISvgService SvgService { get { return ServiceProvider.GetService<ISvgService>(); } }

        public static ISyncService SyncService { get { return ServiceProvider.GetService<ISyncService>(); } }

        public static IThreadService ThreadService { get { return ServiceProvider.GetService<IThreadService>(); } }

        public static UIServices.IUserInterfaceActiveDataService UserInterfaceActiveDataService { get { return ServiceProvider.GetService<UIServices.IUserInterfaceActiveDataService>(); } }

        public static UIServices.IUserInterfaceViewFactoryService UserInterfaceViewFactoryService { get { return ServiceProvider.GetService<UIServices.IUserInterfaceViewFactoryService>(); } }
    }
}
