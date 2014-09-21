using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    internal interface IConnector
    {
        void Bind(IReactor dispatcher, IProtocolFactory factory, IPEndPoint localEP, int bufferSize);
    }
}