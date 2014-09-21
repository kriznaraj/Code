using System;
using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class StreamPeerConnector : IPeerConnector
    {
        private IProtocolFactory _factory;
        private IReactor _dispatcher;
        private Int32 _bufferSize;

        void IPeerConnector.Bind(IReactor dispatcher, IProtocolFactory factory, IPEndPoint localEP, IPEndPoint remoteEP, int bufferSize)
        {
            _factory = factory;
            _dispatcher = dispatcher;
            _bufferSize = bufferSize;

            var tcpClient = new TcpClient(localEP);
            var ar = tcpClient.BeginConnect(remoteEP.Address, remoteEP.Port, null, tcpClient);
            _dispatcher.AddResult(ar, (iar, state) => { this.PeerConnectCallback(iar); }, tcpClient);
        }

        private void PeerConnectCallback(IAsyncResult ar)
        {
            var tcpClient = ((TcpClient)ar.AsyncState);
            tcpClient.EndConnect(ar);

            var protocol = _factory.Create();

            var connection = new StreamConnection(_dispatcher, protocol, tcpClient, _bufferSize);

            protocol.Bind(connection);
            protocol.Connected();

            connection.Start(false);
        }
    }
}