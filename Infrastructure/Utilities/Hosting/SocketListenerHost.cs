using System.Net;
using System.Net.Sockets;
using BallyTech.Infrastructure.Communication;

namespace BallyTech.Infrastructure.Hosting
{
    public sealed class SocketListenerHost : IServiceHost
    {
        private readonly SocketListener _listener;

        public SocketListenerHost(SocketType transportProtocol, IPEndPoint endPoint, IProtocolFactory dataProtocolFactory)
        {
            _listener = new SocketListener(transportProtocol, endPoint, dataProtocolFactory);
        }

        void IServiceHost.Run()
        {
            _listener.Start();
        }

        void IServiceHost.Shutdown()
        {
            _listener.Dispose();
        }
    }
}