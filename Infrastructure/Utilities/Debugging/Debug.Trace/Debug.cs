using System.Collections.Generic;
using Controls.Types;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides a set of properties and methods to get performance counter and trace writer object
    /// </summary>
    public class Debug : CriticalFinalizer, IDebug
    {
        /// <summary>
        /// Key value pair containing id and Trace writer component
        /// </summary>
        private IDictionary<string, ITraceWriter> traceCollection;

        /// <summary>
        /// Represents the default Trace writer component
        /// </summary>
        private ITraceWriter defaultTrace;

        /// <summary>
        /// Initializes the debug component
        /// </summary>
        /// <param name="performanceMonitorConfig">Component that contains the list of performance counters and default performance counter</param>
        public Debug(IDictionary<string, ITraceWriter> traceConfiguration, ITraceWriter defaultWriter)
        {
            this.traceCollection = traceConfiguration;
            this.defaultTrace = defaultWriter;
            this.Register(this.traceCollection.Values);
            this.Register(this.defaultTrace);
        }

        /// <summary>
        /// Returns the trace writer object associated with the id
        /// </summary>
        /// <typeparam name="T">The type of trace data</typeparam>
        /// <param name="traceId">Unique identifier for the trace object</param>
        /// <returns>Trace writer object</returns>
        public ITraceWriter GetTrace(string traceId)
        {
            return this.traceCollection.ContainsKey(traceId) == true ? this.traceCollection[traceId] : this.defaultTrace;
        }
    }
}