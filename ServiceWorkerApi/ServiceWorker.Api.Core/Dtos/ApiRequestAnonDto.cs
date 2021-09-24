using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api
{
    public enum ApiRequestAnonType
    {
        GetInfo,
        GetLastMod,
        GetRefData
    }

    public class ApiRequestAnonDto
    {
        public ApiRequestAnonType? RequestType { get; set; }

        public ApiRequestGetRefDataDto GetRefData { get; set; }
    }

    public class ApiRequestGetRefDataDto
    {
        public string ItemType { get; set; }
    }
}
