using System;

namespace NG.ServiceWorker
{
    public static class Services
    {
        public static IApiService ApiService { get { return ServiceProvider.GetService<IApiService>(); } }

        public static IHttpService HttpService { get { return ServiceProvider.GetService<IHttpService>(); } }

        public static IJsonService JsonService { get { return ServiceProvider.GetService<IJsonService>(); } }
    }
}
