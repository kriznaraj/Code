using System;
using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    public class SocketListener : IProtocolFactory, IDisposable
    {
        private IContext _context;
        private readonly Func<IProtocol> _factory;
        private readonly SocketType _type;
        private readonly IPEndPoint _host;

        public SocketListener(SocketType type, IPEndPoint host, IProtocolFactory factory)
            : this(type, host, factory.Create)
        {
            // _context = this.CreateContext(type, host);
        }

        public SocketListener(SocketType type, IPEndPoint host, Action<Byte[], WritebackHandle> dataReceivedCallback)
            : this(type, host, () => { return new BinaryComm(dataReceivedCallback); })
        {
        }

        public SocketListener(SocketType type, IPEndPoint host, Func<IProtocol> factory)
        {
            _type = type;
            _host = host;
            _factory = factory;
        }

        public void Start()
        {
            _context = this.CreateContext(_type, _host);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        IProtocol IProtocolFactory.Create()
        {
            return _factory();
        }

        private IContext CreateContext(SocketType type, IPEndPoint localEndPoint)
        {
            var context = Context.Create();
            switch (type)
            {
                case SocketType.Dgram: context.BindDatagram(this, localEndPoint); break;
                case SocketType.Stream: context.BindStream(this, localEndPoint); break;
                default: throw new ArgumentException("type");
            }
            return context;
        }

        private class BinaryComm : IProtocol
        {
            private IConnection _connection;
            private readonly Action<Byte[], WritebackHandle> _dataReceivedCallback;

            internal BinaryComm(Action<Byte[], WritebackHandle> dataReceivedCallback)
            {
                _dataReceivedCallback = dataReceivedCallback;
            }

            void IProtocol.Bind(IConnection connection)
            {
                _connection = connection;
            }

            void IProtocol.Connected()
            {
                if (null != _connection.GetPeer())
                    Console.WriteLine("Connection Accepted From [{0}]", _connection.GetPeer());
            }

            void IProtocol.ConnectionLost(CloseConnectionReason reason)
            {
                Console.WriteLine(string.Format("Connection Lost With [{0},{1}]", _connection.GetPeer(), reason));
            }

            void IProtocol.DataReceived(IPEndPoint from, byte[] data, int offset, int size)
            {
                var writeBackHandle = new WritebackHandle(_connection);
                _dataReceivedCallback(data, writeBackHandle);
            }
        }
    }
}