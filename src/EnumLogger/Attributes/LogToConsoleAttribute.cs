using System;

namespace EnumLogger.Attributes
{
    public class LogToConsoleAttribute : Attribute
    {
        public LogToConsoleAttribute()
        {
            if (!log4net.LogManager.GetRepository().Configured)
            {
                log4net.Config.BasicConfigurator.Configure(
                    new log4net.Appender.ConsoleAppender
                    {
                        Layout = new log4net.Layout.SimpleLayout()
                    });
            }
        } 
    }
}