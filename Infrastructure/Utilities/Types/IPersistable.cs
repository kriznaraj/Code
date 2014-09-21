namespace Controls.Types
{
    /// <summary>
    /// Interface to be implemented by a type that has to be persisted in a data store.
    /// </summary>
    public interface IPersistable
    {
        /// <summary>
        /// Gets the object state. Example : New, modified, deleted etc
        /// </summary>
        PersistableState State { get; }

        /// <summary>
        /// Gets the version number of the object
        /// </summary>
        int? Version { get; }
    }
}