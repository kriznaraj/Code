using System.Collections.Generic;

namespace Controls.Types
{
    /// <summary>
    /// Interface to be implemented by a class that converts database model to object model
    /// </summary>
    /// <typeparam name="TReader">Type which reads data from database</typeparam>
    /// <typeparam name="TAggregate">The Entity that has to be filled</typeparam>
    public interface IObjectBuilder<in TReader, TAggregate>
    {
        /// <summary>
        /// Method to convert database model to object model
        /// </summary>
        /// <param name="reader">Type which reads data from database</param>
        /// <returns>List of entity instances</returns>
        IList<TAggregate> Fill(TReader reader);
    }
}