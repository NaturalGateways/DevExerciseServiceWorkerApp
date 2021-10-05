using System;
using System.Collections.Generic;
using System.Linq;

using Natural.Aws.DynamoDB;

namespace ServiceWorker.DynamoDbMaintainer
{
    public class DatabaseUpdater
    {
        /// <summary>The service to use.</summary>
        public Natural.Aws.IAwsService AwsService { get; set; }

        /// <summary>The table name of the DynamoDB table with ref data.</summary>
        public string RefDataTableName { get; set; }

        /// <summary>Updates the database.</summary>
        public void UpdateRefData(RefDataModel.RefData refData)
        {
            // Connect to DB
            using (IDynamoService dynamoDbService = this.AwsService.CreateDynamoService())
            {
                // Create tables
                IDynamoTable refDataTable = dynamoDbService.GetTable(this.RefDataTableName, "RefDataType", "RefDataKey");

                // Get now
                string dbNow = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ");

                // Update forms
                if (refData.FormArray != null)
                {
                    foreach (RefDataModel.RefDataForm form in refData.FormArray)
                    {
                        SetForm(refDataTable, dbNow, form);
                    }
                    SetFormList(refDataTable, dbNow, refData.FormArray.Select(x => x.Key).ToArray());
                }
            }
        }

        /// <summary>Sets a job type.</summary>
        private static void SetForm(IDynamoTable refDataTable, string dbNow, RefDataModel.RefDataForm form)
        {
            refDataTable.PutItemAsync("Form", form.Key, new ItemUpdate
            {
                StringAttributes = new Dictionary<string, string>
                {
                    { "DateTimeUtc", dbNow },
                    { "JsonData", System.Text.Json.JsonSerializer.Serialize(form) }
                }
            }).Wait();
        }

        /// <summary>Sets a job type.</summary>
        private static void SetFormList(IDynamoTable refDataTable, string dbNow, string[] formKeyArray)
        {
            refDataTable.PutItemAsync("Master", "FormList", new ItemUpdate
            {
                StringAttributes = new Dictionary<string, string>
                {
                    { "DateTimeUtc", dbNow },
                    { "JsonData", System.Text.Json.JsonSerializer.Serialize(formKeyArray) }
                }
            }).Wait();
        }

        /// <summary>Updates the database.</summary>
        public void UpdateDatabase()
        {
            // Connect to DB
            using (IDynamoService dynamoDbService = this.AwsService.CreateDynamoService())
            {
                // Create tables
                IDynamoTable refDataTable = dynamoDbService.GetTable(this.RefDataTableName, "RefDataType", "RefDataKey");

                // Get now
                string dbNow = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ");

                // Upload job types
                SetRefDataItem(refDataTable, dbNow, "JobStatus", "PEND", "{\"JobStatusId\":1,\"JobStatusKey\":\"PEND\",\"JobStatusName\":\"Pending\"}");
                SetRefDataItem(refDataTable, dbNow, "JobStatus", "HOLD", "{\"JobStatusId\":2,\"JobStatusKey\":\"HOLD\",\"JobStatusName\":\"On Hold\"}");
                SetRefDataItem(refDataTable, dbNow, "JobStatus", "COMPLETE", "{\"JobStatusId\":3,\"JobStatusKey\":\"COMPLETE\",\"JobStatusName\":\"Complete\"}");
                SetRefDataItem(refDataTable, dbNow, "JobStatus", "CANCELLED", "{\"JobStatusId\":4,\"JobStatusKey\":\"CANCELLED\",\"JobStatusName\":\"Cancelled\"}");
                SetRefDataItem(refDataTable, dbNow, "PaymentType", "Quote", "{\"PaymentTypeId\":1,\"PaymentTypeKey\":\"Quote\",\"PaymentTypeName\":\"Quote\"}");
                SetRefDataItem(refDataTable, dbNow, "PaymentType", "PerHour", "{\"PaymentTypeId\":2,\"PaymentTypeKey\":\"PerHour\",\"PaymentTypeName\":\"Per Hour\"}");
                SetRefDataItem(refDataTable, dbNow, "MaterialType", "1", "{\"MaterialTypeId\":1,\"MaterialTypeName\":\"Nescafe Blend 43, Jar, 300g\"}");
                SetRefDataItem(refDataTable, dbNow, "MaterialType", "2", "{\"MaterialTypeId\":2,\"MaterialTypeName\":\"Sugar, White, 250g\"}");
                SetRefDataItem(refDataTable, dbNow, "MaterialType", "3", "{\"MaterialTypeId\":3,\"MaterialTypeName\":\"Lipton, Black Tea\"}");
                // Upload final times
                SetRefDataItem(refDataTable, dbNow, "Master", "LastUpdated", $"{{\"RefData\":\"{dbNow}\"}}");
            }
        }

        /// <summary>Sets a job type.</summary>
        private static void SetRefDataItem(IDynamoTable refDataTable, string dbNow, string itemType, string itemKey, string jsonData)
        {
            refDataTable.PutItemAsync(itemType, itemKey, new ItemUpdate
            {
                StringAttributes = new Dictionary<string, string>
                    {
                        { "DateTimeUtc", dbNow },
                        { "JsonData", jsonData }
                    }
            }).Wait();
        }
    }
}
