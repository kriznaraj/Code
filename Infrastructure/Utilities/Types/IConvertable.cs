using System.Collections.Generic;

namespace Controls.Types
{
    /// <summary>
    /// Interface that provides a method to return a list of persistable objects
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    public interface IConvertable<TAggregate>
    {
        /// <summary>
        /// Returns a list of persistable objects
        /// </summary>
        /// <param name="aggregate">Type of the aggregate</param>
        /// <returns>List of Persistable objects</returns>
        IEnumerable<IPersistable> ToPersistables(TAggregate aggregate);
    }
}