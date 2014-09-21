using System;
using System.IO;
using System.Net;

namespace BallyTech.Infrastructure.Communication
{
    internal abstract class Connection : IConnection, ITransmitter<WriteBuffer>
    {
        protected readonly IReactor _dispatcher;
        protected readonly IProtocol _protocol;

        protected Boolean _isConnected = false;
        protected TransmissionQueue<WriteBuffer> _transmissionQueue;

        internal Connection(IReactor dispatcher, IProtocol protocol)
        {
            _dispatcher = dispatcher;
            _protocol = protocol;
            _transmissionQueue = new TransmissionQueue<WriteBuffer>(this);
        }

        void IConnection.Write(Byte[] data)
        {
            this.Write(data, null, false);
        }

        void IConnection.Write(Byte[] data, IPEndPoint remote, Boolean awaitReply)
        {
            this.Write(data, remote, awaitReply);
        }

        void IConnection.LostConnection()
        {
            this.LoseConnection(CloseConnectionReason.HostInitiated);
        }

        System.Net.IPEndPoint IConnection.GetHost()
        {
            return this.GetHost();
        }

        System.Net.IPEndPoint IConnection.GetPeer()
        {
            return this.GetPeer();
        }

        void ITransmitter<WriteBuffer>.Transmit(WriteBuffer writeBuffer, Action onComplete)
        {
            this.Transmit(writeBuffer, onComplete);
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Start(Boolean startRead)
        {
            if (_isConnected)
                throw new InvalidOperationException("Connection already establised");

            _isConnected = true;

            if (startRead)
                this.Read(false);
        }

        protected abstract void Read(Boolean awaited);

        protected abstract void Transmit(WriteBuffer writeBuffer, Action onComplete);

        protected abstract void ForceCloseConnection();

        protected abstract IPEndPoint GetHost();

        protected abstract IPEndPoint GetPeer();

        protected abstract void Dispose(Boolean disposing);

        protected void OnSocketClosed(ObjectDisposedException ex)
        {
            this.LoseConnection(CloseConnectionReason.PeerDisconnected);
        }

        protected void OnProtocolException(Exception ex)
        {
            this.LoseConnection(CloseConnectionReason.ProtocolException);
        }

        protected virtual void OnSocketException(Exception ex)
        {
            this.LoseConnection(CloseConnectionReason.SocketExecption);
        }

        protected virtual void OnSocketReadFailed(IOException ex)
        {
            this.LoseConnection(CloseConnectionReason.ClientDisconnectedReadFailed);
        }

        protected virtual void OnSocketWriteFailed(IOException ex)
        {
            this.LoseConnection(CloseConnectionReason.ClientDisconnectedWriteFailed);
        }

        private void Write(Byte[] data, IPEndPoint endPoint, Boolean awaitReply)
        {
            if (!_isConnected) throw new InvalidOperationException("Cannot write to a closed connection.");
            _transmissionQueue.Enqueue(new WriteBuffer(endPoint, data, 0, data.Length, awaitReply));
        }

        private void LoseConnection(CloseConnectionReason reason)
        {
            if (!_isConnected)
                return;

            this.ForceCloseConnection();

            _protocol.ConnectionLost(reason);
            _isConnected = false;
        }
    }
}