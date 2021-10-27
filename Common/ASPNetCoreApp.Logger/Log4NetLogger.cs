using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.Reflection;
using System.ComponentModel;

namespace ASPNetCoreApp.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;

        public Log4NetLogger(string Category,XmlElement Configuration)
        {
            var logger_repository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, Category);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.None => false,
                LogLevel.Trace => _Log.IsDebugEnabled,
                LogLevel.Debug => _Log.IsDebugEnabled,
                LogLevel.Information => _Log.IsInfoEnabled,
                LogLevel.Warning => _Log.IsWarnEnabled,
                LogLevel.Error => _Log.IsErrorEnabled,
                LogLevel.Critical => _Log.IsFatalEnabled,
                _ => throw new InvalidEnumArgumentException(nameof(logLevel), (int)logLevel, typeof(LogLevel))
            };
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
                throw new ArgumentNullException(nameof(formatter));

            if (!IsEnabled(logLevel))
                return;

            string log_string = formatter(state, exception);

            if (string.IsNullOrEmpty(log_string) && exception is not { })
                return;

            switch (logLevel)
            {
                default:
                    throw new InvalidEnumArgumentException(nameof(logLevel),(int)logLevel,typeof(LogLevel));

                case LogLevel.None:
                    break;


                case LogLevel.Trace:
                case LogLevel.Debug:    
                    _Log.Debug(log_string);
                    break;

                case LogLevel.Information:
                    _Log.Info(log_string);
                    break;

                case LogLevel.Warning:
                    _Log.Warn(log_string);
                    break;

                case LogLevel.Error:
                    _Log.Error(log_string);
                    break;

                case LogLevel.Critical:
                    _Log.Fatal(log_string);
                    break;



            }
        }
    }
}
