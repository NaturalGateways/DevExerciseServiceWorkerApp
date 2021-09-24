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
        public async Task<ApiResponseDto> AnonEndpointHandler(Dto.AnonGetRequestDto input, ILambdaContext context)
        {
            // Create service
            AnonGetService getService = new AnonGetService(new Helpers.CloudFormationTableNameProvider())
            {
                AwsService = new Natural.Aws.LambdaAwsService()
            };

            // Execute request
            return await getService.ExecuteRequest(ConvertRequestBody(input.body));
        }

        private ApiRequestAnonDto ConvertRequestBody(Dto.AnonGetRequestBodyDto rawBody)
        {
            // Parse request type
            if (string.IsNullOrEmpty(rawBody.RequestType))
            {
                throw new ApiException("A request type must be provided.");
            }
            ApiRequestAnonType convertedType = ApiRequestAnonType.GetInfo;
            if (Enum.TryParse<ApiRequestAnonType>(rawBody.RequestType, true, out convertedType) == false)
            {
                throw new ApiException($"Request type '{rawBody.RequestType}' is unrecognised.");
            }
            // Create and return converted version
            ApiRequestAnonDto convertedBody = new ApiRequestAnonDto
            {
                RequestType = convertedType,
                GetRefData = rawBody.GetRefData
            };
            return convertedBody;
        }
    }
}
