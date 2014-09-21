using System;

namespace Controls.Printing
{
    /// <summary>
    /// Interface to be implemented by the error print result
    /// </summary>
    public interface IPrintErrorResult : IPrintResult
    {
        /// <summary>
        /// Gets the Reason for the failure
        /// </summary>
        FailReason Reason { get; }

        /// <summary>
        /// Gets the reason code for the failure
        /// </summary>
        string ReasonCode { get; }
    }
}