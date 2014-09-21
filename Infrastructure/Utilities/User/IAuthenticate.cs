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
    /// Interface to be implemented by the authenticator
    /// </summary>
    public interface IAuthenticate
    {
        /// <summary>
        /// Authenticates the user name and password
        /// </summary>
        /// <param name="userName">UserName of the user</param>
        /// <param name="password">Hashed password value</param>
        /// <returns>Returns the list of roles and available login location</returns>
        IResponse<IList<Pair<IRole, IList<ILocation>>>> Authenticate(string userName, string password);
    }
}
