using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api
{
    public enum ApiRequestAnonType
    {
        GetInfo,
        GetLastMod,
        GetRefData,
        GetRefDataItem
    }

    public class ApiRequestAnonDto
    {
        public ApiRequestAnonType? RequestType { get; set; }

        public ApiRequestGetRefDataDto GetRefData { get; set; }
    }

    public class ApiRequestGetRefDataDto
    {
        public string ItemType { get; set; }

        public string ItemKey { get; set; }
    }
}
