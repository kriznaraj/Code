namespace BallyTech.Infrastructure.Communication
{
    /// <summary>
    /// Called by the Dispatcher when a new connection is accepted.
    /// </summary>
    public interface IProtocolFactory
    {
        /// <summary>
        /// Creates a new instance of the protocol.
        /// </summary>
        /// <returns></returns>
        IProtocol Create();
    }
}