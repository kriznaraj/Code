
namespace BallyTech.Infrastructure.Session
{
    /// <summary>
    /// Interface to be implemented by the session
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Currently used asset information
        /// </summary>
        IUserDevice LoggedOnDevice { get; }

        /// <summary>
        /// Currently logged in user.
        /// </summary>
        IUser User { get; }

        /// <summary>
        /// Currently used physical location
        /// </summary>
        ILocation LoggedOnLocation { get; }

        /// <summary>
        /// Currently Logged in Role
        /// </summary>
        IRole Role { get; }

        /// <summary>
        /// Current User Session id
        /// </summary>
        string ID { get; }
    }
}
