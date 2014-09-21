using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class StreamConnection : Connection
    {
        private bool _isDisposed;
        private TcpClient _tcpClient;
        private IPEndPoint _remoteEndPoint;
        private Action _onTransmitCompleteCallback;
        private Byte[] _readBuffer;

        internal StreamConnection(IReactor dispatcher, IProtocol protocol, TcpClient tcpClient, int bufferSize)
            : base(dispatcher, protocol)
        {
            _tcpClient = tcpClient;
            _readBuffer = new Byte[bufferSize];
            _remoteEndPoint = _tcpClient.Client.RemoteEndPoint as IPEndPoint;
        }

        protected override void Read(Boolean awaited)
        {
            try
            {
                if (!_tcpClient.Connected)
                {
                    base.OnSocketReadFailed(new IOException());
                    return;
                }

                var ar = _tcpClient.GetStream().BeginRead(_readBuffer, 0, _readBuffer.Length, null, awaited);
                _dispatcher.AddResult(ar, (iar, o) => { this.ReadCallback(iar); }, null);
            }
            catch (SocketException ex)
            {
                if (ShouldIgnoreError(ex.SocketErrorCode))
                {
                    this.Read(awaited);
                    return;
                }

                base.OnSocketException(ex);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                base.OnSocketClosed(ex);
                return;
            }
            catch (IOException ex)
            {
                base.OnSocketReadFailed(ex);
                return;
            }
        }

        protected override void ForceCloseConnection()
        {
            this.Dispose(true);
        }

        protected override System.Net.IPEndPoint GetHost()
        {
            return _tcpClient.Client.LocalEndPoint as IPEndPoint;
        }

        protected override System.Net.IPEndPoint GetPeer()
        {
            return _remoteEndPoint;
        }

        protected override void Transmit(WriteBuffer writeBuffer, Action onComplete)
        {
            IAsyncResult ar;
            _onTransmitCompleteCallback = onComplete;

            try
            {
                ar = _tcpClient.GetStream().BeginWrite(
                        writeBuffer.Data.Array,
                        writeBuffer.Data.Offset,
                        writeBuffer.Data.Count,
                        null,
                        writeBuffer.AwaitReply);
            }
            catch (SocketException ex)
            {
                base.OnSocketException(ex);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                base.OnSocketClosed(ex);
                return;
            }
            catch (IOException ex)
            {
                base.OnSocketWriteFailed(ex);
                return;
            }

            _dispatcher.AddResult(ar, (iar, o) => { this.WriteCallback(iar); }, null);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isConnected = false;

                if (disposing)
                {
                    if (_tcpClient != null)
                    {
                        _tcpClient.Close();
                    }
                }

                _tcpClient = null;
                _onTransmitCompleteCallback = null;
                _isDisposed = true;
            }
        }

        private bool ShouldIgnoreError(SocketError error)
        {
            switch (error)
            {
                case SocketError.ConnectionReset:
                case SocketError.ConnectionRefused:
                    return true;

                default: return false;
            }
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            if (_isDisposed)
            {
                return;
            }

            Boolean awaited = (Boolean)asyncResult.AsyncState;

            Int32 bytesRead;

            try
            {
                if (!_isConnected)
                {
                    if (_tcpClient != null)
                    {
                        _tcpClient.Close();
                        _tcpClient = null;
                    }

                    return;
                }

                bytesRead = _tcpClient.GetStream().EndRead(asyncResult);
            }
            catch (SocketException ex)
            {
                if (ShouldIgnoreError(ex.SocketErrorCode))
                {
                    this.Read(awaited);
                    return;
                }

                base.OnSocketException(ex);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                base.OnSocketClosed(ex);
                return;
            }
            catch (IOException ex)
            {
                base.OnSocketReadFailed(ex);
                return;
            }

            try
            {
                Byte[] buff = new Byte[bytesRead];
                Array.Copy(_readBuffer, buff, bytesRead);
                _protocol.DataReceived(_remoteEndPoint, buff, 0, bytesRead);
            }
            catch (Exception ex)
            {
                base.OnProtocolException(ex);
            }

            if (awaited)
                _onTransmitCompleteCallback();
            else
                this.Read(false);
        }

        private void WriteCallback(IAsyncResult ar)
        {
            try
            {
                _tcpClient.GetStream().EndWrite(ar);
            }
            catch (SocketException ex)
            {
                if (ShouldIgnoreError(ex.SocketErrorCode))
                {
                    _onTransmitCompleteCallback();
                    return;
                }

                base.OnSocketException(ex);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                base.OnSocketClosed(ex);
                return;
            }
            catch (IOException ex)
            {
                base.OnSocketWriteFailed(ex);
                return;
            }

            var awaitReply = (Boolean)ar.AsyncState;
            if (awaitReply)
            {
                this.Read(true);
            }
            else
            {
                _onTransmitCompleteCallback();
            }
        }
    }
}