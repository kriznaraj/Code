using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    internal interface IPeerConnector
    {
        void Bind(IReactor dispatcher, IProtocolFactory factory, IPEndPoint localEP, IPEndPoint remoteEP, int bufferSize);
    }
}