using System;
using System.Runtime.Serialization;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides a method to handle the listeners who monitor the trace and debug output
    /// </summary>
    public interface ITraceHandler : IDisposable
    {
        /// <summary>
        /// Handles the listeners who monitor the trace and debug output
        /// </summary>
        /// <typeparam name="T">The type of data to be written to the trace file</typeparam>
        /// <param name="data">Trace object containing trace id, trace event type and data</param>
        void Handle<T>(ITrace<T> data) where T : ISerializable;
    }
}