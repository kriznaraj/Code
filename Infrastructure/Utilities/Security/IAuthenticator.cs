using System.Collections.Generic;
using Controls.Types;

namespace Controls.Security
{
    /// <summary>
    /// Interface to be implemented by the authenticator
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Authenticates the user name and password
        /// </summary>
        /// <param name="userName">UserName of the user</param>
        /// <param name="password">Hashed password value</param>
        /// <returns>Returns the list of Sites and available roles</returns>
        IResponse<IList<Pair<ISite, IList<IRole>>>> AuthenticateAndGetAccess(string userName, string password);

        /// <summary>
        /// Authenticates the user name and password
        /// </summary>
        /// <param name="userName">UserName of the user</param>
        /// <param name="password">Hashed password value</param>
        /// <returns>Returns the User with his personal details</returns>
        IResponse<IUser> Authenticate(string userName, string password);
        
    }
}