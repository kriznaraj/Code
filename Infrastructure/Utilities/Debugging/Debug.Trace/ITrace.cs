using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Controls.Debugging
{
    /// <summary>
    /// Contains properties of the trace data to be written to the trace file
    /// </summary>
    /// <typeparam name="T">The type of the trace data</typeparam>
    public interface ITrace<T>
        where T : ISerializable
    {
        /// <summary>
        /// Data to be written to the trace file
        /// </summary>
        T Data { get; }

        /// <summary>
        /// Unique identifier for the Trace
        /// </summary>
        Int32 ID { get; }

        /// <summary>
        /// The type of event that has caused the trace
        /// </summary>
        TraceEventType TraceEventType { get; }
    }
}