using System;
using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    public class Context : IContext
    {
        private readonly IReactor reactor;

        public static IContext Create()
        {
            return new Context();
        }

        void IContext.BindDatagram(IProtocolFactory factory, IPEndPoint local)
        {
            Connector.BindUdp(this.reactor, factory, local);
        }

        void IContext.BindStream(IProtocolFactory factory, IPEndPoint local)
        {
            Connector.BindTcp(this.reactor, factory, local);
        }

        void IContext.ConnectDatagram(IProtocolFactory factory, IPEndPoint local, IPEndPoint remote)
        {
            Connector.ConnectUdp(this.reactor, factory, local, remote);
        }

        void IContext.ConnectStream(IProtocolFactory factory, IPEndPoint local, IPEndPoint remote)
        {
            Connector.ConnectTcp(this.reactor, factory, local, remote);
        }

        void IDisposable.Dispose()
        {
            this.Terminate();
        }

        private Context()
        {
            this.reactor = new Reactor();
            this.Start();
        }

        private void Start()
        {
            this.reactor.Run();
        }

        private void Terminate()
        {
            this.reactor.Shutdown();
        }
    }
}