namespace Controls.Types
{
    /// <summary>
    /// Interface to be implemented by a class which provides DML statements.
    /// </summary>
    /// <typeparam name="TEntityObject">Instance of the entity</typeparam>
    /// <typeparam name="TCommand">Command object</typeparam>
    public interface ICommandBuilder<in TEntityObject, out TCommand>
    {
        /// <summary>
        /// Provides command for inserting an entity
        /// </summary>
        /// <param name="aggregate">Instance of the entity</param>
        /// <returns>Insert command</returns>
        TCommand CreateInsertCommand(TEntityObject aggregate);

        /// <summary>
        /// Provides command for updating an entity
        /// </summary>
        /// <param name="aggregate">Instance of the entity</param>
        /// <returns>Update command</returns>
        TCommand CreateUpdateCommand(TEntityObject aggregate);

        /// <summary>
        /// Provides command for deleting an entity.
        /// </summary>
        /// <param name="aggregate">Instance of the entity</param>
        /// <returns>Delete command</returns>
        TCommand CreateDeleteCommand(TEntityObject aggregate);

        /// <summary>
        /// Gets the rank of the entity which specifies the order in which the commands have to be executed.
        /// </summary>
        short Rank { get; }
    }
}