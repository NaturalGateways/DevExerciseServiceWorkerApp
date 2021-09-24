using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceWorker.Api.Lambda.Dto
{
    public class AnonGetRequestDto
    {
        public AnonGetRequestBodyDto body { get; set; }
    }

    public class AnonGetRequestBodyDto
    {
        public string RequestType { get; set; }

        public ApiRequestGetRefDataDto GetRefData { get; set; }
    }
}
