using System.Diagnostics;
using System.Security.Permissions;

namespace Controls.Debugging
{
    [SecurityPermission(SecurityAction.Assert)]
    /// <summary>
    /// Provides a set of methods to manipulate the custom performance counters
    /// </summary>
    internal class Counter : ICounter
    {
        /// <summary>
        /// Unique identifier for a performance counter
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Represents a performance counter component
        /// </summary>
        private PerformanceCounter performanceCounter;

        /// <summary>
        /// Initializes the performance counter component
        /// </summary>
        /// <param name="id">Unique identifier for a performance counter</param>
        /// <param name="categoryName">The category of performance counter</param>
        /// <param name="counterName">Name of the performance counter</param>
        /// <param name="instanceName">Performance counter instance</param>
        /// <param name="machineName"> The computer on which the performance counter and its associated                   category exists</param>
        /// <param name="counterLifeTime">Specifies the life time of performance counter instance</param>
        internal Counter(int id, string categoryName, string counterName, string instanceName, string machineName, PerformanceCounterInstanceLifetime counterLifeTime)
        {
            this.performanceCounter = new PerformanceCounter()
            {
                CategoryName = categoryName,
                CounterName = counterName,
                InstanceName = instanceName,
                InstanceLifetime = counterLifeTime,
                MachineName = machineName,
                ReadOnly = false
            };

            this.ID = id;
        }

        /// <summary>
        /// Initializes the performance counter component
        /// </summary>
        /// <param name="id">Unique identifier for a performance counter</param>
        /// <param name="categoryName">The category of performance counter</param>
        /// <param name="counterName">Name of the performance counter</param>
        /// <param name="instanceName">Performance counter instance</param>
        internal Counter(int id, string categoryName, string counterName, string instanceName)
            : this(id, categoryName, counterName, instanceName, ".", PerformanceCounterInstanceLifetime.Process)
        {
        }

        /// <summary>
        /// Increments the performance counter value by 1
        /// </summary>
        public void Increment()
        {
            this.IncrementBy(1);
        }

        /// <summary>
        /// Decrements the performance counter value by 1
        /// </summary>
        public void Decrement()
        {
            this.performanceCounter.Decrement();
        }

        /// <summary>
        /// Increments the performance counter by a given value
        /// </summary>
        /// <param name="valueToIncrement"></param>
        public void IncrementBy(long valueToIncrement)
        {
            this.performanceCounter.IncrementBy(valueToIncrement);
        }

        /// <summary>
        /// Sets the performance counter value to zero
        /// </summary>
        public void Reset()
        {
            this.performanceCounter.RawValue = 0;
        }

        /// <summary>
        /// Releases the unmanaged resources associated with the performance counter component and optionally             releases the managed resources
        /// </summary>
        public void Dispose()
        {
            this.performanceCounter.Dispose();
            this.performanceCounter = null;
        }
    }
}