using System;

namespace Controls.Logging
{
    /// <summary>
    /// Interface to be implemented by the formatter
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public interface IFormatter
    {
        /// <summary>
        /// Convert the object into the string
        /// </summary>
        /// <param name="instance">Instance to convert to</param>
        /// <returns>Return the string data</returns>
        String ToString(object type);
    }
}