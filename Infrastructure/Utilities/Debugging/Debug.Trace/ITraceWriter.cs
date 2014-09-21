using System;
using System.Runtime.Serialization;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides method to write the trace file
    /// </summary>
    public interface ITraceWriter : IDisposable
    {
        /// <summary>
        /// Writes the data to the trace file
        /// </summary>
        /// <typeparam name="T">The type of data to be written to the trace file</typeparam>
        /// <param name="trace">Trace object containing trace id, trace event type and data</param>
        void Write<T>(ITrace<T> trace) where T : ISerializable;
    }
}