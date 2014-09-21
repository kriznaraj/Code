using System.Collections.Generic;

namespace Controls.Logging
{
    /// <summary>
    /// Interface to be implemented by the Message Provider
    /// </summary>
    public interface IMessageProvider
    {
        /// <summary>
        /// Gets the message for the given id
        /// </summary>
        /// <param name="messageId">ID of the message</param>
        /// <param name="props">Property dictionary collection</param>
        /// <returns>Returns the message string</returns>
        string GetMessage(long messageId, IDictionary<string, string> props);
    }
}