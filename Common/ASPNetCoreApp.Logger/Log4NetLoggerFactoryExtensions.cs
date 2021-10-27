using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace ASPNetCoreApp.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory,string ConfigurationFile = "log4net.config")
        {
            Factory.AddProvider(new Log4NetLoggerProvider(CheckPath(ConfigurationFile)));
            return Factory;
        }

        private static string CheckPath(string configurationFile)
        {
            if (configurationFile is not { Length: > 0 })
                throw new ArgumentException("Can't find configuration file");

            if (Path.IsPathRooted(configurationFile))
                return configurationFile;

            Assembly assembly = Assembly.GetEntryAssembly();
            var path = Path.GetDirectoryName(assembly!.Location);

            return Path.Combine(path!, configurationFile);

        }
    }
}
