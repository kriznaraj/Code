using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class DatagramConnector : IConnector
    {
        private UdpClient _udpClient;

        void IConnector.Bind(IReactor dispatcher, IProtocolFactory factory, IPEndPoint localEP, int bufferSize)
        {
            _udpClient = new UdpClient(localEP);
            _udpClient.Client.ReceiveBufferSize = bufferSize;

            var protocol = factory.Create();

            var connection = new DatagramConnection(dispatcher, protocol, _udpClient);
            connection.Start(true);

            protocol.Bind(connection);
            protocol.Connected();
        }
    }
}