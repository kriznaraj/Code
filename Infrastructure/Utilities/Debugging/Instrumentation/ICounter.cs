using System;

namespace Controls.Debugging
{
    /// <summary>
    /// Provides set of methods to manipulate performance counter object
    /// </summary>
    public interface ICounter : IDisposable
    {
        /// <summary>
        /// Unique Identifier for a performance counter
        /// </summary>
        Int32 ID { get; }

        /// <summary>
        /// Increments the performance counter value by 1
        /// </summary>
        void Increment();

        /// <summary>
        /// Decrements the performance counter value by 1
        /// </summary>
        void Decrement();

        /// <summary>
        /// Increments the performance counter by a given value
        /// </summary>
        /// <param name="valueToIncrement">The value by which the counter has to be incremented</param>
        void IncrementBy(long valueToIncrement);

        /// <summary>
        /// Sets the performance counter value to zero
        /// </summary>
        void Reset();
    }
}