using System;
using System.Collections.Generic;

namespace Controls.Logging
{
    /// <summary>
    /// Logger interface for Semantic Logging
    /// </summary>
    public interface ISemanticLog
    {
        /// <summary>
        /// Log message as given log type to given source
        /// </summary>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] props to be used logged</param>
        /// <param name="logType">[Optional] Log type for the entry</param>
        void Log(Int64 messageId, IDictionary<string, string> props = null, LogType logType = LogType.Info);

        /// <summary>
        /// Log message as given log type to given source
        /// </summary>
        /// <param name="obj">Object to be logged</param>
        /// <param name="logType">[Optional] Log type for the entry</param>
        void Log<T>(T obj, LogType logType = LogType.Info);

        /// <summary>
        /// Log message as debug info to given source
        /// </summary>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void Debug(Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as debug info to given source
        /// </summary>
        /// <param name="obj">Object to be logged</param>
        void Debug<T>(T obj);

        /// <summary>
        /// Log message as error to given source
        /// </summary>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void Error(Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as error to given source
        /// </summary>
        /// <param name="obj">Object to be logged</param>
        void Error<T>(T obj);

        /// <summary>
        /// Log message as fatal to given source
        /// </summary>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void Fatal(Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as fatal to given source
        /// </summary>
        /// <param name="obj">Object to be logged</param>
        void Fatal<T>(T obj);

        /// <summary>
        /// Log message as informational message to given source
        /// </summary>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void Info(Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as informational message to given source
        /// </summary>
        /// <param name="obj">Object to be logged</param>
        void Info<T>(T obj);

        /// <summary>
        /// Log message as warning to given source
        /// </summary>
        /// <param name="messageId">ID of the message to be logged</param>
        /// <param name="props">[Optional] Exception to be logged</param>
        void Warning(Int64 messageId, IDictionary<string, string> props = null);

        /// <summary>
        /// Log message as warning to given source
        /// </summary>
        /// <param name="obj">Object to be logged</param>
        void Warning<T>(T obj);
    }
}