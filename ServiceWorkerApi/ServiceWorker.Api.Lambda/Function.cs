using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ServiceWorker.Api.Lambda
{
    public class Function
    {
        public class AnonGetRequestDto
        {
            public ApiRequestAnonDto body { get; set; }
        }

        public async Task<ApiResponseDto> AnonEndpointHandler(AnonGetRequestDto input, ILambdaContext context)
        {
            // Create service
            AnonGetService getService = new AnonGetService
            {
                AwsService = new Natural.Aws.LambdaAwsService()
            };

            // Execute request
            return await getService.ExecuteRequest(input.body);
        }
    }
}
