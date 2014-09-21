using System.Collections.Generic;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides methods to get the list of trace writers and default trace writer
    /// </summary>
    public interface ITraceConfiguration
    {
        /// <summary>
        /// To get the list of trace writers
        /// </summary>
        /// <returns>Key value pair consisting of Id and trace writer</returns>
        IDictionary<string, ITraceWriter> GetTraceCollection();

        /// <summary>
        /// To get the default trace writer
        /// </summary>
        /// <returns>Default trace writer</returns>
        ITraceWriter GetDefaultTrace();
    }
}