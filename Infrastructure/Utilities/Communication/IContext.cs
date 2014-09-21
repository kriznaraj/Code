using System;
using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    public interface IContext : IDisposable
    {
        void BindDatagram(IProtocolFactory factory, IPEndPoint local);

        void BindStream(IProtocolFactory factory, IPEndPoint local);

        void ConnectDatagram(IProtocolFactory factory, IPEndPoint local, IPEndPoint remote);

        void ConnectStream(IProtocolFactory factory, IPEndPoint local, IPEndPoint remote);
    }
}