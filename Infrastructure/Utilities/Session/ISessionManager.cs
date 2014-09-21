using BallyTech.Infrastructure.Types;

namespace BallyTech.Infrastructure.Session
{
    /// <summary>
    /// Interface to be implemented by session manager that manages the user session
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Generates a new user session for the given inputs
        /// </summary>
        /// <param name="user">user to whom the session has to be generated</param>
        /// <param name="role">role used by the user for login</param>
        /// <param name="location">location in which the user logged in</param>
        /// <returns>Returns the session generated for the employee</returns>
        ISession Create(IUser user, IRole role, ILocation location);

        /// <summary>
        /// Provides the security context that is applicable for the given session
        /// </summary>
        /// <param name="sessionId">Current used Session Id</param>
        /// <returns>Returns the security context for the given session</returns>
        IResponse<ISession> FindSession(string sessionId);
    }
}
