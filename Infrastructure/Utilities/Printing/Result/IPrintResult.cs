using System;

namespace Controls.Printing
{
    /// <summary>
    /// Interface to be implemented by the print result
    /// </summary>
    public interface IPrintResult
    {
        /// <summary>
        /// Gets the Print Job Id for which the response is sent
        /// </summary>
        PrintJobId PrintJobId { get; }

        /// <summary>
        /// Gets the status of the printing
        /// </summary>
        PrintStatus Status { get; }
    }
}