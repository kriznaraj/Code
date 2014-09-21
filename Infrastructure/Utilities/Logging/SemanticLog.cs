using System;
using System.Collections.Generic;

namespace Controls.Logging
{
    public class SemanticLog : ISemanticLog
    {
        private readonly string source;
        private readonly ILogger logger;
        private readonly LogType logtype;

        public SemanticLog(String source, ILogger logger)
        {
            this.source = source;
            this.logger = logger;
            this.logtype = (LogType)this.logger.GetLogLevel(source);
        }

        private void Log(long messageId, IDictionary<string, string> props, LogType logType)
        {
            if ((this.logtype & logType) == logType)
            {
                this.logger.Log(source, messageId, props, logType);
            }
        }

        private void Log<T>(T obj, LogType logType)
        {
            if ((this.logtype & logType) == logType)
            {
                this.logger.Log(source, obj, logType);
            }
        }

        void ISemanticLog.Log(long messageId, IDictionary<string, string> props, LogType logType)
        {
            this.Log(messageId, props, logType);
        }

        void ISemanticLog.Log<T>(T obj, LogType logType)
        {
            this.Log(obj, logType);
        }

        void ISemanticLog.Debug(long messageId, IDictionary<string, string> props)
        {
            this.Log(messageId, props, LogType.Debug);
        }

        void ISemanticLog.Debug<T>(T obj)
        {
            this.Log(obj, LogType.Debug);
        }

        void ISemanticLog.Error(long messageId, IDictionary<string, string> props)
        {
            this.Log(messageId, props, LogType.Error);
        }

        void ISemanticLog.Error<T>(T obj)
        {
            this.Log(obj, LogType.Error);
        }

        void ISemanticLog.Fatal(long messageId, IDictionary<string, string> props)
        {
            this.Log(messageId, props, LogType.Fatal);
        }

        void ISemanticLog.Fatal<T>(T obj)
        {
            this.Log(obj, LogType.Fatal);
        }

        void ISemanticLog.Info(long messageId, IDictionary<string, string> props)
        {
            this.Log(messageId, props, LogType.Info);
        }

        void ISemanticLog.Info<T>(T obj)
        {
            this.Log(obj, LogType.Info);
        }

        void ISemanticLog.Warning(long messageId, IDictionary<string, string> props)
        {
            this.Log(messageId, props, LogType.Warning);
        }

        void ISemanticLog.Warning<T>(T obj)
        {
            this.Log(obj, LogType.Warning);
        }
    }
}
