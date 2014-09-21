using BallyTech.Infrastructure.Authorization;
using BallyTech.Infrastructure.Session;
using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Authorization
{
    /// <summary>
    /// Interface to be implemented by the ExecutionContextProvider
    /// </summary>
    public interface IExecutionContextProvider
    {
        /// <summary>
        /// Authorizes the incoming request using the incoming session context
        /// </summary>
        /// <param name="sessionContext">security context to be used for the validation</param>
        /// <param name="userContext">user context for the execution</param>
        /// <param name="securityConfig">security configuration for the API</param>
        /// <returns>Returns the execution context</returns>
        IExecutionContext GetExecutionContext(ISession sessionContext, IUserContext userContext, IOperationSecurityConfig securityConfig);
    }
}