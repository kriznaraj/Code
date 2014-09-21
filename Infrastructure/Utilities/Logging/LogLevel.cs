namespace Controls.Logging
{
    /// <summary>
    /// Log Level to be enabled for the Logger
    /// </summary>
    public enum LogLevel
    {
        Invalid = 0,

        /// <summary>
        /// Log Fatal Message Only
        /// </summary>
        FatalOnly = LogType.Fatal,

        /// <summary>
        /// Log Messages till Error Message
        /// </summary>
        ErrorLevel = LogType.Fatal | LogType.Error,

        /// <summary>
        /// Log Messages till Warning Message
        /// </summary>
        WarningLevel = LogType.Fatal | LogType.Error | LogType.Warning,

        /// <summary>
        /// Log Messages till debug message
        /// </summary>
        DebugLevel = LogType.Fatal | LogType.Error | LogType.Warning | LogType.Debug,

        /// <summary>
        /// Log Messages till information message
        /// </summary>
        InformationLevel = LogType.Fatal | LogType.Error | LogType.Warning | LogType.Debug | LogType.Info,

        /// <summary>
        /// Log All the message in the logger
        /// </summary>
        All = InformationLevel,
    }
}