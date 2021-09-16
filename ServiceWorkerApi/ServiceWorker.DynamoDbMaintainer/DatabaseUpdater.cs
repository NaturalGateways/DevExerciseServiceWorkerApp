using System;
using System.Collections.Generic;
using System.Text;

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
                SetJobStatus(refDataTable, dbNow, "PEND", "Pending");
                SetJobStatus(refDataTable, dbNow, "HOLD", "On Hold");
                SetJobStatus(refDataTable, dbNow, "COMPLETE", "Complete");
                SetJobStatus(refDataTable, dbNow, "CANCELLED", "Cancelled");
            }
        }

        /// <summary>Sets a job type.</summary>
        private static void SetJobStatus(IDynamoTable refDataTable, string dbNow, string jobStatusKey, string jobStatusName)
        {
            // Create JSON data
            Dictionary<string, object> jsonDataObj = new Dictionary<string, object>
            {
                { "JobStatusKey", jobStatusKey },
                { "JobStatusName", jobStatusName }
            };
            string jsonDataString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonDataObj);

            // Put item
            refDataTable.PutItemAsync("JobStatus", jobStatusKey, new ItemUpdate
            {
                StringAttributes = new Dictionary<string, string>
                    {
                        { "DateTimeUtc", dbNow },
                        { "JsonData", jsonDataString }
                    }
            }).Wait();
        }
    }
}
