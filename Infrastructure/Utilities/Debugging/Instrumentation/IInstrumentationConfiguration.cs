using System.Collections.Generic;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides methods to get the list of performance counters and default performance counter
    /// </summary>
    public interface IInstrumentationConfiguration
    {
        /// <summary>
        /// To get the list of performance counters
        /// </summary>
        /// <returns>Key value pair consisting of Id and Performance Counter</returns>
        IDictionary<string, ICounter> GetCounterCollection();

        /// <summary>
        /// To get the default performance counter
        /// </summary>
        /// <returns>Default performance counter</returns>
        ICounter GetDefaultCounter();
    }
}