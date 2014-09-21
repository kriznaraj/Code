using Controls.Configuration;
using Controls.Types;

namespace Controls.Logging
{
    public class LoggerFactory
    {
        public static ILogger Create(IConfigService configService, IMessageProvider messageProvider)
        {
            var configuration = configService.Get<LoggerConfiguration>("Logging", "Logging");
            configuration.Fill();

            IFormatProvider formatProvider = new FormatProvider(
                configuration.GetDefaultTypeFormatter(),
                configuration.GetDefaultFieldFormatter(),
                configuration.GetTypeFormatterCollection(),
                configuration.GetFieldFormatterCollection());

            ILogging logging = TypeFactory.CreateInstance<ILogging>(configuration.GetLoggerType(),
                configuration.GetLoggerPropertyCollection());

            return new Logger(logging, formatProvider, messageProvider, configuration.GetLoggerLevelCollection());
        }
    }
}