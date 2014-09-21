using System;

namespace Controls.Logging
{
    /// <summary>
    /// Interface to be implemented by the Logging Application
    /// </summary>
    public interface ILogging : IDisposable
    {
        /// <summary>
        /// Logging depth of the current Logger
        /// </summary>
        LogLevel LogLevel { get; }

        /// <summary>
        /// Log the entry
        /// </summary>
        /// <param name="logEntry">Entry to log</param>
        void Log(LogEntry logEntry);
    }
}