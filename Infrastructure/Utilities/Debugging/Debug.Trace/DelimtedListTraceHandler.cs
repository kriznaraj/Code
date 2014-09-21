﻿using System.Diagnostics;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides method to write trace messages in text format separated by delimiter
    /// </summary>
    internal class DelimtedListTraceHandler : Writer
    {
        /// <summary>
        /// Initializes trace handler to write trace data separated by a specified delimiter
        /// </summary>
        /// <param name="traceSource">Trace source that holds a set of handlers</param>
        /// <param name="filterLevels">The level of trace message filtered by trace listener</param>
        /// <param name="traceOptions">Trace data options that has to be written in the trace output</param>
        /// <param name="traceFileFormat">File format component for the handler</param>
        public DelimtedListTraceHandler(TraceSource traceSource, SourceLevels filterLevels, TraceOptions traceOptions, ITraceFileFormat traceFileFormat)
            : base(traceSource, filterLevels, traceOptions, traceFileFormat)
        {
        }

        /// <summary>
        /// Derived method to initialize trace listener to write trace data separated by a delimiter
        /// </summary>
        /// <param name="traceSource">Trace source that holds a set of handlers</param>
        /// <param name="filterLevels">The level of trace message filtered by trace listener</param>
        /// <param name="traceOptions">Trace data options that has to be written in the trace output</param>
        /// <param name="traceListener">Trace listener object associated with the trace source</param>
        public override void InitListener(out TraceListener traceListener)
        {
            traceListener = new DelimitedListTraceListener(this.GetStreamWriter());
        }
    }
}