using System;
using System.Threading;

namespace Controls.Logging
{
    /// <summary>
    /// Individual Log entry to be supplied for the logger
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new instance of Logger
        /// </summary>
        private LogEntry()
        {
            this.ManagedThreadId = Thread.CurrentThread.ManagedThreadId;
            this.Timestamp = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Get the type of the log entry
        /// </summary>
        public LogType LogType { get; private set; }

        /// <summary>
        /// Managed thread id from which the log entry is created
        /// </summary>
        public Int32 ManagedThreadId { get; private set; }

        /// <summary>
        /// Instance of the object to be logger
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Source to which the log entry has to be logged
        /// </summary>
        public String Source { get; private set; }

        /// <summary>
        /// Log entry created Date Time Offset
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// Creates a new instance of LogEntry of type string
        /// </summary>
        /// <param name="source">Source of the Logger</param>
        /// <param name="message">Message to be logged</param>
        /// <param name="logType">Type of the log entry</param>
        /// <param name="props">Property collection dictionary</param>
        /// <returns>returns the new LogEntry</returns>
        public static LogEntry CreateLogEntry(String source, String message, LogType logType)
        {
            return new LogEntry { Source = source, LogType = logType, Message = message };
        }
    }
}