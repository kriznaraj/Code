using System.Collections.Generic;
using Controls.Types;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides a set of properties and methods to get performance counter and trace writer object
    /// </summary>
    public class Instrumentation : CriticalFinalizer, IInstrumentation
    {
        /// <summary>
        /// Key value pair containing id and performance counter component
        /// </summary>
        private IDictionary<string, ICounter> counterCollection;

        /// <summary>
        /// Represents the default performance counter component
        /// </summary>
        private ICounter defaultCounter;

        /// <summary>
        /// Initializes the Instrumentation component
        /// </summary>
        /// <param name="performanceMonitorConfig">Component that contains the list of performance counters and default performance counter</param>
        public Instrumentation(IDictionary<string, ICounter> performanceMonitorConfig, ICounter defaultCounter)
        {
            this.counterCollection = performanceMonitorConfig;
            this.defaultCounter = defaultCounter;
            this.Register(this.counterCollection.Values);
            this.Register(this.defaultCounter);
        }

        /// <summary>
        ///  Returns the performance counter object associated with the id
        /// </summary>
        /// <param name="counterId">Unique identifier for the counter</param>
        /// <returns>Performance counter object</returns>
        public ICounter GetCounter(string counterId)
        {
            return this.counterCollection.ContainsKey(counterId) == true ? this.counterCollection[counterId] : this.defaultCounter;
        }
    }
}