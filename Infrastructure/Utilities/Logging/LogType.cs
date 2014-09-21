using System;

namespace Controls.Logging
{
    /// <summary>
    /// Log Type for the Logger as Flag
    /// </summary>
    [Flags]
    public enum LogType
    {
        Invalid = 0,

        /// <summary>
        /// Log Type is Fatal Error
        /// </summary>
        Fatal = 1,

        /// <summary>
        /// Log Type is an Error Message
        /// </summary>
        Error = 2,

        /// <summary>
        /// Log Type is an Warning Message
        /// </summary>
        Warning = 4,

        /// <summary>
        /// Log Type is an Debugging Message
        /// </summary>
        Debug = 8,

        /// <summary>
        /// Log Type is an Information Message
        /// </summary>
        Info = 16,
    }
}