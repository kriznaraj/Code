using System;

namespace BallyTech.Infrastructure.Communication
{
    public sealed class WritebackHandle
    {
        private readonly IConnection _connection;

        internal WritebackHandle(IConnection connection)
        {
            _connection = connection;
        }

        public void Write(Byte[] data)
        {
            _connection.Write(data, _connection.GetPeer());
        }
    }
}