using System;
using System.Collections.Generic;
using System.Diagnostics;
using Controls.Types;

namespace Controls.Debugging
{
    /// <summary>
    /// Class that Provides a method to handle the listeners who monitor the trace and debug output
    /// </summary>
    internal class TraceHandler : CriticalFinalizer, ITraceHandler
    {
        /// <summary>
        /// Multicast Delegate that holds reference to the set of method that hanldes the stream writer for each handler
        /// </summary>
        private Action handleStreamWriter;

        /// <summary>
        /// Trace source that holds a set of handlers
        /// </summary>
        private TraceSource traceSource;

        /// <summary>
        /// Initializes Trace source and associates the list of handlers attached to it
        /// </summary>
        /// <param name="traceSource">Trace source that holds a set of handlers</param>
        /// <param name="traceHandlerStreamWriter">TracehandlerStreamWriter object</param>
        public TraceHandler(TraceSource traceSource, IList<Writer> traceHandlerStreamWriter)
        {
            this.traceSource = traceSource;
            foreach (Writer writer in traceHandlerStreamWriter)
            {
                this.handleStreamWriter += new Action(writer.HandleStreamWriter);
            }

            this.Register(traceHandlerStreamWriter);
        }

        /// <summary>
        /// Handles the listeners who monitor the trace and debug output
        /// </summary>
        /// <typeparam name="T">The type of data to be written to the trace file</typeparam>
        /// <param name="data">Trace object containing trace id, trace event type and data</param>
        public void Handle<T>(ITrace<T> data) where T : System.Runtime.Serialization.ISerializable
        {
            this.handleStreamWriter();
            if (this.traceSource != null)
            {
                this.traceSource.TraceData(data.TraceEventType, data.ID, data.Data);
                this.traceSource.Flush();
            }
        }
    }
}