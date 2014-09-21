using Controls.Security;

namespace Controls.Types
{
    /// <summary>
    /// Interface to be used by the execution context
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Gets the current security context
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Gets the current user context
        /// </summary>
        IUserContext UserContext { get; }

        /// <summary>
        /// Currently Running Transaction
        /// </summary>
        IOperation Operation { get; }
    }
}