using System;
using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class StreamConnector : IConnector
    {
        private TcpListener _tcpListener;
        private IProtocolFactory _factory;
        private IReactor _dispatcher;
        private Int32 _bufferSize;

        void IConnector.Bind(IReactor dispatcher, IProtocolFactory factory, IPEndPoint localEP, int bufferSize)
        {
            _factory = factory;
            _dispatcher = dispatcher;
            _bufferSize = bufferSize;

            _tcpListener = new TcpListener(localEP);
            _tcpListener.Start();

            this.StartAccept();
        }

        private void StartAccept()
        {
            var ar = _tcpListener.BeginAcceptTcpClient(null, _tcpListener);
            _dispatcher.AddResult(ar, (iar, state) => { this.ClientAcceptCallback(iar); }, _tcpListener);
        }

        private void ClientAcceptCallback(IAsyncResult ar)
        {
            var tcpClient = ((TcpListener)ar.AsyncState).EndAcceptTcpClient(ar);
            var protocol = _factory.Create();

            var connection = new StreamConnection(_dispatcher, protocol, tcpClient, _bufferSize);
            connection.Start(true);

            protocol.Bind(connection);
            protocol.Connected();

            this.StartAccept();
        }
    }
}