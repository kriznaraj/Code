using System;
using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    public interface IProtocol
    {
        void Bind(IConnection connection);

        void Connected();

        void ConnectionLost(CloseConnectionReason reason = CloseConnectionReason.Unknown);

        void DataReceived(IPEndPoint from, Byte[] data, Int32 offset, Int32 size);
    }

    public enum CloseConnectionReason
    {
        Unknown = 0,
        ProtocolException,
        HostInitiated,
        PeerDisconnected,
        SocketExecption,

        ClientDisconnectedReadFailed,
        ClientDisconnectedWriteFailed,
    }
}