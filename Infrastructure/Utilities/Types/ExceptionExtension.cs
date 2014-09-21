using System;

namespace Controls.Types
{
    public static class ExceptionExtension
    {
        private const string ExceptionFormat = "Exception = {0}";

        private const string InnerExceptionFormat = "Inner Exception = {0}";

        /// <summary>
        /// Constructs an exception message that contains entire exception information including inner exception
        /// </summary>
        /// <param name="exception">Exception object for which the message needs to be constructed</param>
        /// <param name="format">[Optional] Format for the exception string</param>
        /// <returns>Exception as an display message string</returns>
        public static string GetExceptionMessage(this Exception exception, string format = ExceptionFormat)
        {
            return string.Format(format, exception.ToString()
                + Environment.NewLine
                + (exception.InnerException != null ? exception.InnerException.GetExceptionMessage(InnerExceptionFormat) : string.Empty));
        }
    }
}