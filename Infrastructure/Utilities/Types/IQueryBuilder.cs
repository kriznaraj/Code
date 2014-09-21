namespace Controls.Types
{
    /// <summary>
    /// Interface to be implemented by a class which provides DDL statements.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command w.r.t database provider</typeparam>
    /// <typeparam name="TAggregate">Type of the entity for which data has to be retrieved</typeparam>
    /// <typeparam name="TAggregateId">Type that uniquely identifies the entity</typeparam>
    public interface IQueryBuilder<TCommand, TAggregate, TAggregateId>
        where TAggregate : IAggregate<TAggregateId>
    {
        /// <summary>
        /// Provides command for retrieving data with a given entity Id.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns>Select Command</returns>
        TCommand Create(TAggregateId aggregateId);

        /// <summary>
        /// Provides command for retrieving data with a given search criteria
        /// </summary>
        /// <param name="criteia"></param>
        /// <returns>Select Command</returns>
        TCommand Create(IQueryCriteria criteria);
    }
}