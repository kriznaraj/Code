using System;
using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    /// <summary>
    /// Represents a pyhsical connection to with an endpoint.
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Write data to the underlying connection, in a non-blocking fashion.
        /// </summary>
        /// <param name="data"></param>
        void Write(Byte[] data);

        /// <summary>
        /// Write data to the a specific remote connection, in a non-blocking fashion.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="remote"></param>
        /// <param name="awaitReply"></param>
        void Write(Byte[] data, IPEndPoint remote, Boolean awaitReply = false);

        /// <summary>
        /// Flushes the queued data buffer and the closes the underlying connection.
        /// </summary>
        void LostConnection();

        /// <summary>
        /// Gets the Endpoint of host side of the connection.
        /// </summary>
        /// <returns></returns>
        IPEndPoint GetHost();

        /// <summary>
        /// Gets the Endpoint of the remote side of the connection.
        /// </summary>
        /// <returns></returns>
        IPEndPoint GetPeer();
    }
}