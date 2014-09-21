using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Controls.Logging
{
    public class Logger : ILogger
    {
        private AutoResetEvent autoResetEvent;
        private IFormatProvider formatProvider;
        private Thread loggerThread;
        private ILogging logging;
        private LogType logLevel;
        private IMessageProvider messageProvider;
        private ConcurrentQueue<LogEntry> queue;
        private bool run;
        private readonly IDictionary<string, LogLevel> logLevelConfig;

        public Logger(ILogging logging, IFormatProvider formatProvider, IMessageProvider messageProvider, IDictionary<string, LogLevel> logLevelConfig)
        {
            this.logging = logging;
            this.formatProvider = formatProvider;
            this.messageProvider = messageProvider;
            this.logLevelConfig = logLevelConfig;
            this.queue = new ConcurrentQueue<LogEntry>();
            this.run = true;
            this.loggerThread = new Thread(new ThreadStart(this.DoLog)) { IsBackground = true, Priority = ThreadPriority.Lowest, Name = "LoggerThread" };
            this.autoResetEvent = new AutoResetEvent(false);
            this.logLevel = (LogType)this.logging.LogLevel;
            this.Start();
        }

        ~Logger()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Log(string source, long messageId, IDictionary<string, string> props = null, LogType logType = LogType.Info)
        {
            this.Enqueue(this.CreateLogEntry(messageId, props, source, logType));
        }

        public void Log<T>(string source, T obj, LogType logType = LogType.Info)
        {
            this.Enqueue(this.CreateLogEntry(obj, source, logType));
        }

        public void LogDebug(string source, long messageId, IDictionary<string, string> props = null)
        {
            if (DoLog(LogType.Debug))
            {
                this.Log(source, messageId, props, LogType.Debug);
            }
        }

        public void LogDebug<T>(string source, T obj)
        {
            if (DoLog(LogType.Debug))
            {
                this.Log(source, obj, LogType.Debug);
            }
        }

        public void LogError(string source, long messageId, IDictionary<string, string> props = null)
        {
            if (DoLog(LogType.Error))
            {
                this.Log(source, messageId, props, LogType.Error);
            }
        }

        public void LogError<T>(string source, T obj)
        {
            if (DoLog(LogType.Error))
            {
                this.Log(source, obj, LogType.Error);
            }
        }

        public void LogFatal(string source, long messageId, IDictionary<string, string> props = null)
        {
            if (DoLog(LogType.Fatal))
            {
                this.Log(source, messageId, props, LogType.Fatal);
            }
        }

        public void LogFatal<T>(string source, T obj)
        {
            if (DoLog(LogType.Fatal))
            {
                this.Log(source, obj, LogType.Fatal);
            }
        }

        public void LogInfo(string source, long messageId, IDictionary<string, string> props = null)
        {
            if (DoLog(LogType.Info))
            {
                this.Log(source, messageId, props, LogType.Info);
            }
        }

        public void LogInfo<T>(string source, T obj)
        {
            if (DoLog(LogType.Info))
            {
                this.Log(source, obj, LogType.Info);
            }
        }

        public void LogWarning(string source, long messageId, IDictionary<string, string> props = null)
        {
            if (DoLog(LogType.Warning))
            {
                this.Log(source, messageId, props, LogType.Warning);
            }
        }

        public void LogWarning<T>(string source, T obj)
        {
            if (DoLog(LogType.Warning))
            {
                this.Log(source, obj, LogType.Warning);
            }
        }

        public void ShutDown()
        {
            this.Dispose();
        }

        private void Start()
        {
            this.loggerThread.Start();
        }

        private bool DoLog(LogType logType)
        {
            return (this.logLevel & logType) == logType;
        }

        private LogEntry CreateLogEntry(long messageId, IDictionary<string, string> props, string source, LogType logType)
        {
            for (int init = 0; init < props.Count; init++)
            {
                string key = props.Keys.ElementAt(init);
                props[key] = this.formatProvider.GetFormatProvider(key).ToString(props.Values.ElementAt(init));
            }

            return LogEntry.CreateLogEntry(source, this.messageProvider.GetMessage(messageId, props), logType);
        }

        private LogEntry CreateLogEntry<T>(T instance, string source, LogType logType)
        {
            return LogEntry.CreateLogEntry(source, this.formatProvider.GetFormatProvider<T>().ToString(instance), logType);
        }

        private void Dispose(bool disposing)
        {
            this.autoResetEvent.Set();
            this.run = false;

            if (disposing)
            {
                this.loggerThread.Join();
                GC.SuppressFinalize(this);
            }

            this.logging.Dispose();
        }

        private void DoLog()
        {
            while (this.run)
            {
                this.autoResetEvent.WaitOne();

                while (this.queue.Count > 0)
                {
                    LogEntry logEntry;
                    if (this.queue.TryDequeue(out logEntry))
                    {
                        this.logging.Log(logEntry);
                    }
                }
            }
        }

        private void Enqueue(LogEntry logEntry)
        {
            this.queue.Enqueue(logEntry);
            this.autoResetEvent.Set();
        }

        public LogLevel GetLogLevel(string name)
        {
            LogLevel logLevel;
            if (false == this.logLevelConfig.TryGetValue(name, out logLevel))
            {
                logLevel = this.logging.LogLevel;
            }

            return logLevel;
        }
    }
}