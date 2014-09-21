using System;
using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class WriteBuffer
    {
        private readonly IPEndPoint _endPoint;
        private readonly ArraySegment<byte> _data;
        private readonly Boolean _awaitReply;

        internal WriteBuffer(Byte[] data, int offset, int length, Boolean awaitReply)
            : this(null, data, offset, length, awaitReply)
        {
        }

        public WriteBuffer(IPEndPoint endPoint, Byte[] data, int offset, int length, Boolean awaitReply)
        {
            _endPoint = endPoint;
            _awaitReply = awaitReply;
            _data = new ArraySegment<byte>(data, offset, length);
        }

        public IPEndPoint EndPoint
        {
            get { return _endPoint; }
        }

        public ArraySegment<byte> Data
        {
            get { return _data; }
        }

        public Boolean AwaitReply
        {
            get { return _awaitReply; }
        }

        public WriteBuffer WithConsumedBytes(int bytesToConsume)
        {
            if (bytesToConsume > _data.Count)
            {
                throw new InvalidOperationException(
                    "An attempt was made to consume more bytes from a socket transmit buffer than are in the buffer.");
            }

            return new WriteBuffer(_endPoint, _data.Array, _data.Offset + bytesToConsume,
                _data.Count - bytesToConsume, false);
        }
    }
}