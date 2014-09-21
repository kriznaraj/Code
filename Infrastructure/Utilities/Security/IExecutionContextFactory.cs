using Controls.Types;

namespace Controls.Security
{
    /// <summary>
    /// Interface to Create an Execution Context for the a given user, session, operation
    /// </summary>
    public interface IExecutionContextFactory
    {
        /// <summary>
        /// Creates a new context
        /// </summary>
        /// <param name="sessionContext">security context to be used for the validation</param>
        /// <param name="userContext">user context for the execution</param>
        /// <param name="securityConfig">security configuration for the API</param>
        /// <returns>Returns the execution context</returns>
        IExecutionContext Create(ISession sessionContext, IUserContext userContext, IOperationSecurityConfig securityConfig);
    }
}