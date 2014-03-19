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
            Get["Environment/{Environment}/Period/{Period}/Level/{Level}"] = x =>
            {
                LogModel[] logModel = CreateLogModel(x);
                return Response.AsJson(logModel);
            };

            
            Get["/{Environment}/{Period}/{Level}"] = x => 
            {
                LogModel[] logModel = CreateLogModel(x);
                return Response.AsJson(logModel);
            };

            Get["View/Environment/{Environment}/Period/{Period}/Level/{Level}"] = x =>
            {
                LogModel[] logModel = CreateLogModel(x);
                return logModel;
            };


            Get["View/{Environment}/{Period}/{Level}"] = x =>
            {
                LogModel[] logModel = CreateLogModel(x);
                return logModel;
            };

        }

        private static LogModel[] CreateLogModel(dynamic x)
        {
            string environment = x.Environment;
            LogPeriod period = (LogPeriod)Enum.Parse(typeof(LogPeriod), x.Period, ignoreCase: true);
            int level = int.Parse(x.Level);
            LogQuery query = new LogQuery();
            var logModel = query.Execute(environment, period, level);
            return logModel;
        }
    }
}