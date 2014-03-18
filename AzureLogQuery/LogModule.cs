using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureLogQuery
{
    public class LogModule : NancyModule
    {
        public LogModule()
        {
            Get["/{Environment}/{Period}/{Level}"] = x => 
            {
                string environment = x.Environment;
                LogPeriod period = Enum.Parse(typeof(LogPeriod), x.Period, ignoreCase: true);
                int level = int.Parse(x.Level); 
                LogQuery query = new LogQuery();
                return Response.AsJson(query.Execute(environment, period, level));
            };

            Get["View/{Environment}/{Period}/{Level}"] = x =>
            {
                string environment = x.Environment;
                LogPeriod period = Enum.Parse(typeof(LogPeriod), x.Period, ignoreCase: true);
                int level = int.Parse(x.Level);
                LogQuery query = new LogQuery();
                return query.Execute(environment, period, level);
            };

        }
    }
}