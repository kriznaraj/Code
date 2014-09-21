using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class DatagramPeerConnector : IPeerConnector
    {
        private UdpClient _udpClient;

        void IPeerConnector.Bind(IReactor dispatcher, IProtocolFactory factory, IPEndPoint localEP, IPEndPoint remoteEP, int bufferSize)
        {
            _udpClient = new UdpClient(localEP);
            _udpClient.Client.ReceiveBufferSize = bufferSize;
            _udpClient.Connect(remoteEP);

            var protocol = factory.Create();

            var connection = new DatagramConnection(dispatcher, protocol, _udpClient);
            connection.Start(false);

            protocol.Bind(connection);
            protocol.Connected();
        }
    }
}