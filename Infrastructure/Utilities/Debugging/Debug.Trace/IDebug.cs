using System;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides a set of methods to get performance counter and trace writer object
    /// </summary>
    public interface IDebug : IDisposable
    {
        /// <summary>
        /// Returns the trace writer object associated with the id
        /// </summary>
        /// <typeparam name="T">The type of trace data</typeparam>
        /// <param name="traceId">Unique identifier for the trace object</param>
        /// <returns>Trace writer object</returns>
        ITraceWriter GetTrace(string traceId);
    }
}