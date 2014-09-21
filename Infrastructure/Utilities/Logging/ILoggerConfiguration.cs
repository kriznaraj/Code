using System.Collections.Generic;

namespace Controls.Logging
{
    public interface ILoggerConfiguration
    {
        string GetDefaultFieldFormatter();

        string GetDefaultTypeFormatter();

        IDictionary<string, string> GetFieldFormatterCollection();

        IDictionary<string, string> GetLoggerPropertyCollection();

        string GetLoggerType();

        IDictionary<string, string> GetTypeFormatterCollection();

        IDictionary<string, LogLevel> GetLoggerLevelCollection();
    }
}