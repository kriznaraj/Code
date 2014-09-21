namespace Controls.Types
{
    /// <summary>
    /// Interface to be implemented by class which represents a database Entity.
    /// Example: Player, Award.
    /// </summary>
    /// <typeparam name="TAggregateId">Type to uniquely identify the Entity</typeparam>
    public interface IAggregate<TAggregateId>
    {
        /// <summary>
        /// Gets an identifier to uniquely identify the Entity.
        /// </summary>
        TAggregateId Id { get; }
    }
}