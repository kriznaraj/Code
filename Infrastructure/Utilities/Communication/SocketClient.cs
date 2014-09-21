using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BallyTech.Infrastructure.Communication
{
    public class SocketClient : IProtocol, IProtocolFactory, IDisposable
    {
        private Byte[] _buffer;
        private IConnection _connection;

        private readonly TimeSpan _timeout;
        private readonly IContext _context;
        private readonly AutoResetEvent _signal = new AutoResetEvent(false);

        public SocketClient(SocketType type, IPEndPoint local, IPEndPoint remote, TimeSpan timeout)
        {
            _timeout = timeout;
            _context = this.CreateContext(type, local, remote);
        }

        private IContext CreateContext(SocketType type, IPEndPoint local, IPEndPoint remote)
        {
            var context = Context.Create();
            switch (type)
            {
                case SocketType.Dgram: context.ConnectDatagram(this, local, remote); break;
                case SocketType.Stream: context.ConnectStream(this, local, remote); break;
                default: throw new ArgumentException("type");
            }

            var signalled = _signal.WaitOne(_timeout);
            if (!signalled)
                throw new TimeoutException("Connection timed out");

            return context;
        }

        public void Send(Byte[] data)
        {
            _connection.Write(data, _connection.GetPeer());
        }

        public Byte[] SendAndReceive(Byte[] data)
        {
            _connection.Write(data, _connection.GetPeer(), true);

            var signalled = _signal.WaitOne(_timeout);
            if (!signalled)
                throw new TimeoutException("Received timed out");

            return _buffer;
        }

        void IProtocol.Bind(IConnection connection)
        {
            if (null != connection.GetPeer())
                Console.WriteLine(string.Format("Connection to :{0}", connection.GetPeer()));

            _connection = connection;
            _signal.Set();
        }

        void IProtocol.Connected()
        {
        }

        void IProtocol.ConnectionLost(CloseConnectionReason reason)
        {
            Console.WriteLine(string.Format("Connection lost due to:{0}", reason));
        }

        void IProtocol.DataReceived(IPEndPoint from, byte[] data, int offset, int size)
        {
            _buffer = data;
            _signal.Set();
        }

        IProtocol IProtocolFactory.Create()
        {
            return this;
        }

        void IDisposable.Dispose()
        {
            _connection.LostConnection();
        }
    }
}