using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    internal static class Connector
    {
        internal static void BindTcp(IReactor reactor, IProtocolFactory protocolFactory, IPEndPoint localEP, int bufferSize = 8192)
        {
            IConnector connector = new StreamConnector();
            connector.Bind(reactor, protocolFactory, localEP, bufferSize);
        }

        internal static void BindUdp(IReactor reactor, IProtocolFactory protocolFactory, IPEndPoint localEP, int bufferSize = 524288)
        {
            IConnector connector = new DatagramConnector();
            connector.Bind(reactor, protocolFactory, localEP, bufferSize);
        }

        internal static void ConnectTcp(IReactor reactor, IProtocolFactory protocolFactory, IPEndPoint localEP, IPEndPoint remoteEP, int bufferSize = 8192)
        {
            IPeerConnector connector = new StreamPeerConnector();
            connector.Bind(reactor, protocolFactory, localEP, remoteEP, bufferSize);
        }

        internal static void ConnectUdp(IReactor reactor, IProtocolFactory protocolFactory, IPEndPoint localEP, IPEndPoint remoteEP, int bufferSize = 524288)
        {
            IPeerConnector connector = new DatagramPeerConnector();
            connector.Bind(reactor, protocolFactory, localEP, remoteEP, bufferSize);
        }
    }
}