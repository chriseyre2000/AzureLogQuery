using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AzureLogQuery
{
    public class LogQuery
    {
        public LogModel[] Query(string environment, LogPeriod period, int level)
        {
            string connectionString = ConfigurationManager.AppSettings[environment];

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable cloudTable = tableClient.GetTableReference("WADLogsTable");

            TableQuery query = new TableQuery();

            string pkFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThan, PartitionKey(period));
            string levelFilter = TableQuery.GenerateFilterConditionForInt("Level", QueryComparisons.LessThanOrEqual, level);
            string combinedFilter = TableQuery.CombineFilters(pkFilter, TableOperators.And, levelFilter);
            query.FilterString = combinedFilter;

            LogModel[] items = cloudTable.ExecuteQuery(query).Select(x => new LogModel
                {
                    PartitionKey = x.PartitionKey,
                    RowKey = x.RowKey,
                    Level = x.Properties["Level"].Int32Value.Value,
                    Timestamp = x.Timestamp.UtcDateTime,
                    Message = x.Properties["Message"].StringValue,
                }).ToArray();

            return items;
        }

        public string PartitionKey(LogPeriod period)
        { 
            DateTime datetime = DateTime.UtcNow;

            switch (period)
            { 
                case LogPeriod.Ever:
                    datetime = datetime.AddYears(-20);
                    break;
                case LogPeriod.LastDay:
                    datetime = datetime.AddDays(-1);
                    break;
                case LogPeriod.LastHour:
                    datetime = datetime.AddHours(-1);
                    break;
            }

            return string.Format("0{0}", datetime.Ticks);
        }
    }
}