using System;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides a set of methods to get performance counter and trace writer object
    /// </summary>
    public interface IInstrumentation : IDisposable
    {
        /// <summary>
        /// Returns the performance counter object associated with the id
        /// </summary>
        /// <param name="counterId">Unique identifier for the counter</param>
        /// <returns>Performance counter object</returns>
        ICounter GetCounter(string counterId);
    }
}