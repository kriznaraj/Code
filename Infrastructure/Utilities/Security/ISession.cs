namespace Controls.Security
{
    /// <summary>
    /// Interface to be implemented by the session
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Current User Session id
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Currently used device information
        /// </summary>
        IUserDevice LoggedInDevice { get; }

        /// <summary>
        /// Currently loggedin user.
        /// </summary>
        IUser LoggedInUser { get; }

        /// <summary>
        /// Currently loggedin location
        /// </summary>
        ILocation LoggedInLocation { get; }

        /// <summary>
        /// Currently Logged in Role
        /// </summary>
        IRole LoggedInRole { get; }

        /// <summary>
        /// Currently Logged in Site
        /// </summary>
        ISite LoggedInSite { get; }
    }
}