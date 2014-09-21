using System;

namespace Controls.Printing
{
    /// <summary>
    /// Interface to be implemented by the success print result
    /// </summary>
    public interface IPrintSuccessResult : IPrintResult
    {
        /// <summary>
        /// Gets the Time of the printing of the job
        /// </summary>
        DateTimeOffset PrintedDateTime { get; set; }
    }
}