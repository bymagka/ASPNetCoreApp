using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ASPNetCoreApp.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string configurationFile;
        private readonly ConcurrentDictionary<string,Log4NetLogger> Loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        public Log4NetLoggerProvider(string ConfigurationFile)
        {
            configurationFile = ConfigurationFile;
        }

        public ILogger CreateLogger(string categoryName)
        {
           return Loggers.GetOrAdd(categoryName, category =>
            {
                var xml = new XmlDocument();
                xml.Load(configurationFile);
                return new Log4NetLogger(category,xml["configuration"]["log4net"]);
            });
        }

        public void Dispose()
        {
            Loggers.Clear();
        }
    }
}
