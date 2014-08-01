using System;
using System.Configuration;
using System.Linq;
using log4net;
using enum_logger.Enums;

namespace enum_logger.Extensions
{
    // Looks up the logger and log level for each request
    // Performance for logger lookup is insignificant according to http://geekswithblogs.net/mapfel/archive/2011/08/02/146412.aspx
    public static class EnumLogger
    {
        private const string EventId = "EventId";
        private const string EventName = "EventName";

        static EnumLogger()
        {
            log4net.GlobalContext.Properties.Clear();
        }

        public static void Log (
            this Enum logEvent,
            object callingClass,
            string message,
            params Object[] messageParameters)
        {
            var noOfExtraParameters = messageParameters.Length;
            var exception = noOfExtraParameters > 0
                ? messageParameters[noOfExtraParameters - 1] as Exception
                : null;

            var stringFormatParameters = exception == null
                ? messageParameters
                : messageParameters.Take(noOfExtraParameters - 1).ToArray();

            var logString = stringFormatParameters.Any()
                ? string.Format(message, messageParameters)
                : message;

            if (exception == null)
            {
                LogViaLog4Net(callingClass, logEvent, logString);
            }
            else
            {
                LogViaLog4Net(callingClass, logEvent, logString + " - " + exception, exception);
            } 
        }

        private static void LogViaLog4Net(object callingClass,
            Enum logEvent,
            string message,
            Exception exception = null)
        {
            var level = GetCurrentEventLogLevel(logEvent);
            var logger = callingClass is Type
                ? LogManager.GetLogger(callingClass as Type)
                : LogManager.GetLogger(callingClass.GetType());

            var enumValue = Convert.ChangeType(logEvent, logEvent.GetType());
            MDC.Set(EventId, enumValue.ToString());
            var enumName = logEvent.GetName();
            MDC.Set(EventName, enumName);
            var logMessage = string.Join(enumName, ": ", message);

            switch (level)
            {
                case LogLevel.Debug:
                    logger.Debug(logMessage, exception);
                    break;

                case LogLevel.Info:
                    logger.Info(logMessage, exception);
                    break;

                case LogLevel.Warn:
                    logger.Warn(logMessage, exception);
                    break;

                case LogLevel.Error:
                    logger.Error(logMessage, exception);
                    break;

                case LogLevel.Fatal:
                    logger.Fatal(logMessage, exception);
                    break;
            }
            MDC.Remove(EventId);
            MDC.Remove(EventName);
        }

        private static LogLevel GetCurrentEventLogLevel(Enum logEvent)
        {
            var enumName = logEvent.GetType().Name.Split('.').Last();
            var level = ConfigurationManager.AppSettings.Get(enumName + "." + logEvent.GetName());
            return
                level == "Debug" ? LogLevel.Debug : 
                level == "Info" ? LogLevel.Info :
                level == "Warn" ? LogLevel.Warn :
                level == "Error" ? LogLevel.Error :
                level == "Fatal" ? LogLevel.Fatal :
                LogLevel.Debug;
        }

        private static string GetName(this Enum currentEnum)
        {
            return Enum.GetName(currentEnum.GetType(), currentEnum);
        }

    }
}
