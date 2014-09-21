using BallyTech.Infrastructure.Authorization;
using BallyTech.Infrastructure.Session;
using BallyTech.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.User
{
    /// <summary>
    /// Interface to be implemented by the authorizer
    /// </summary>
    public interface IAuthorize
    {
        /// <summary>
        /// Authorizes the incoming request using the incoming session context
        /// </summary>
        /// <param name="sessionContext">security context to be used for the validation</param>
        /// <param name="securityConfig">security config for the given api</param>
        /// <returns>Returns the execution context</returns>
        IResponse<IUserContext> Authorize(ISession sessionContext, IOperationSecurityConfig securityConfig);
    }
}
