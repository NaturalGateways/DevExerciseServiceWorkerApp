using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Natural.Aws.DynamoDB;
using Natural.Aws;

namespace ServiceWorker.Api
{
    public class AnonGetService
    {
        /// <summary>The table name provider.</summary>
        public ITableNameProvider TableNameProvider { get; private set; }

        /// <summary>The AWS service.</summary>
        public IAwsService AwsService { get; set; }

        /// <summary>Constructor.</summary>
        public AnonGetService()
        {
            this.TableNameProvider = new TableNameProviders.StubTableNameProvider();
        }

        /// <summary>Constructor.</summary>
        public AnonGetService(ITableNameProvider provider)
        {
            this.TableNameProvider = provider;
        }

        /// <summary>Executes a request.</summary>
        public async Task<ApiResponseDto> ExecuteRequest(ApiRequestAnonDto requestDto)
        {
            try
            {
                // Get response
                object responseObject = null;
                switch (requestDto.RequestType)
                {
                    case ApiRequestAnonType.GetInfo:
                        responseObject = ExecuteGetInfoRequest();
                        break;
                    case ApiRequestAnonType.GetLastMod:
                        responseObject = await ExecuteGetLastMod();
                        break;
                    case ApiRequestAnonType.GetRefData:
                        responseObject = await ExecuteGetRefDataRequestAsync(requestDto.GetRefData);
                        break;
                    default:
                        throw new Exception($"Request type '{requestDto.RequestType}' unrecognised.");
                }
                // Ensure response is deserialised properly
                return new ApiResponseDto { Success = true, Response = responseObject };
            }
            catch (ApiException ae)
            {
                ApiResponseDto responseDto = new ApiResponseDto { Success = false, ErrorMessage = ae.UserMessage, ErrorException = new List<ApiResponseExceptionDto>() };
                Exception ex = ae;
                while (ex != null)
                {
                    responseDto.ErrorException.Add(new ApiResponseExceptionDto
                    {
                        TypeName = ex.GetType().FullName,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    });
                    ex = ex.InnerException;
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                ApiResponseDto responseDto = new ApiResponseDto { Success = false, ErrorException = new List<ApiResponseExceptionDto>() };
                while (ex != null)
                {
                    responseDto.ErrorException.Add(new ApiResponseExceptionDto
                    {
                        TypeName = ex.GetType().FullName,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    });
                    ex = ex.InnerException;
                }
                return responseDto;
            }
        }

        /// <summary>Executes a get-info request.</summary>
        private static object ExecuteGetInfoRequest()
        {
            // Get the resource stream
            System.IO.Stream resourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ServiceWorker.Api.Resources.AppInfo.json");
            if (resourceStream == null)
            {
                throw new Exception("Cannot retrieve app info JSON");
            }

            // Read the version
            string appInfoString = null;
            using (System.IO.StreamReader appInfoStream = new System.IO.StreamReader(resourceStream))
            {
                appInfoString = appInfoStream.ReadToEnd();
            }
            Dictionary<string, string> appInfoJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(appInfoString);

            // Add author and return
            return new Dictionary<string, object>
            {
                { "Name", appInfoJson["Name"] },
                { "Version", appInfoJson["Version"] },
                { "Author", "Steven Moore" },
                { "Environment", Environment.GetEnvironmentVariable("Environment") },
                { "Copyright", "2021" },
                { "InterfaceVersion", 1 }
            };
        }

        /// <summary>Executes a get-ref-data request.</summary>
        private async Task<object> ExecuteGetLastMod()
        {
            // Check AWS service
            IAwsService awsService = this.AwsService;
            if (awsService == null)
            {
                throw new Exception("Cannot get ref data without an AWS service.");
            }

            // Read data
            using (IDynamoService dynamoDbService = awsService.CreateDynamoService())
            {
                string tableInstanceName = this.TableNameProvider.GetInstanceNameFromConceptName("RefData");
                IDynamoTable refDataTable = dynamoDbService.GetTable(tableInstanceName, "RefDataType", "RefDataKey");
                IDynamoItem item = await refDataTable.GetItemByKeyAsync("Master", "LastUpdated", "JsonData");
                return item.GetStringAsObject<object>("JsonData");
            }
        }

        /// <summary>Executes a get-ref-data request.</summary>
        private async Task<object> ExecuteGetRefDataRequestAsync(ApiRequestGetRefDataDto requestDto)
        {
            // Check AWS service
            IAwsService awsService = this.AwsService;
            if (awsService == null)
            {
                throw new Exception("Cannot get ref data without an AWS service.");
            }

            // Read data
            using (IDynamoService dynamoDbService = awsService.CreateDynamoService())
            {
                string tableInstanceName = this.TableNameProvider.GetInstanceNameFromConceptName("RefData");
                IDynamoTable refDataTable = dynamoDbService.GetTable(tableInstanceName, "RefDataType", "RefDataKey");
                IEnumerable<IDynamoItem> items = await refDataTable.GetItemsAsync(requestDto.ItemType, null, "JsonData");
                string[] itemDataArray2 = items.Select(x => x.GetString("JsonData")).ToArray();
                object item1 = items.ToArray()[0].GetStringAsObject<object>("JsonData");
                object item2 = items.ToArray()[1].GetStringAsObject<object>("JsonData");
                object item3 = items.ToArray()[2].GetStringAsObject<object>("JsonData");
                object item4 = items.ToArray()[3].GetStringAsObject<object>("JsonData");
                object[] itemDataArray = items.Select(x => x.GetStringAsObject<object>("JsonData")).ToArray();
                return itemDataArray;
            }
        }
    }
}
