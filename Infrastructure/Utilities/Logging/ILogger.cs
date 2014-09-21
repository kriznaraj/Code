using System;
using System.Collections.Generic;

namespace Controls.Logging
{
    /// <summary>
    /// Logger interface for Logging
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Get the logger level defined for the given source
        /// </summary>
        /// <param name="name">name of the source</param>
        /// <returns>returns the log level defined for the source</returns>
        LogLevel GetLogLevel(string name);

        /// <summary>
        /// Log message as given log type to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] props to be used logged</param>
        /// <param name="logType">[Optional] Log type for the entry</param>
        void Log(String source, Int64 messageId, IDictionary<string, string> props = null, LogType logType = LogType.Info);

        /// <summary>
        /// Log message as given log type to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="obj">Object to be logged</param>
        /// <param name="logType">[Optional] Log type for the entry</param>
        void Log<T>(String source, T obj, LogType logType = LogType.Info);

        /// <summary>
        /// Log message as debug info to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void LogDebug(String source, Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as debug info to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="obj">Object to be logged</param>
        void LogDebug<T>(String source, T obj);

        /// <summary>
        /// Log message as error to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void LogError(String source, Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as error to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="obj">Object to be logged</param>
        void LogError<T>(String source, T obj);

        /// <summary>
        /// Log message as fatal to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void LogFatal(String source, Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as fatal to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="obj">Object to be logged</param>
        void LogFatal<T>(String source, T obj);

        /// <summary>
        /// Log message as informational message to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void LogInfo(String source, Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as informational message to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="obj">Object to be logged</param>
        void LogInfo<T>(String source, T obj);

        /// <summary>
        /// Log message as warning to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void LogWarning(String source, Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as warning to given source
        /// </summary>
        /// <param name="source">Logger Source for logging</param>
        /// <param name="obj">Object to be logged</param>
        void LogWarning<T>(String source, T obj);

        /// <summary>
        /// Stops the Logging
        /// </summary>
        void ShutDown();

    }
}