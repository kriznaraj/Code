using System;
using Controls.Types;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides properties and method to write the trace file
    /// </summary>
    internal class TraceWriter : CriticalFinalizer, ITraceWriter
    {
        /// <summary>
        /// Unique identifier for a trace component
        /// </summary>
        private string id;

        /// <summary>
        /// Represents a trace handler component
        /// </summary>
        private ITraceHandler traceHandler;

        /// <summary>
        /// Intializes
        /// </summary>
        /// <param name="_id">Unique identifier for a trace component</param>
        /// <param name="_traceHandler">Represents a trace handler component</param>
        public TraceWriter(string _id, ITraceHandler _traceHandler)
        {
            if (_traceHandler == null)
            {
                throw new ArgumentNullException("_traceHandler", "traceHandler cannot be null");
            }

            this.id = _id;
            this.traceHandler = _traceHandler;
            this.Register(this.traceHandler);
        }

        /// <summary>
        /// Writes the data to the trace file
        /// </summary>
        /// <typeparam name="T">The type of data to be written to the trace file</typeparam>
        /// <param name="trace">Trace object containing trace id, trace event type and data</param>
        public void Write<T>(ITrace<T> trace) where T : System.Runtime.Serialization.ISerializable
        {
            traceHandler.Handle<T>(trace);
        }
    }
}