using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureLogQuery
{
    public class LogModel
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
        public int Level { get; set; }
        public string Message { get; set; }
    }
}