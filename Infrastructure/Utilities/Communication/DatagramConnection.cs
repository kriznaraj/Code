using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class DatagramConnection : Connection
    {
        private bool _isDisposed;
        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;
        private Action _onTransmitCompleteCallback;

        internal DatagramConnection(IReactor dispatcher, IProtocol protocol, UdpClient udpClient)
            : base(dispatcher, protocol)
        {
            _udpClient = udpClient;
        }

        protected override void Read(Boolean awaited)
        {
            try
            {
                var ar = _udpClient.BeginReceive(null, awaited);
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
            return _udpClient.Client.LocalEndPoint as IPEndPoint;
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
                if (writeBuffer.EndPoint == null)
                {
                    ar = _udpClient.BeginSend(
                            writeBuffer.Data.Array,
                            writeBuffer.Data.Count,
                            null,
                            writeBuffer.AwaitReply);
                }
                else
                {
                    ar = _udpClient.BeginSend(
                            writeBuffer.Data.Array,
                            writeBuffer.Data.Count,
                            writeBuffer.EndPoint,
                            null,
                            writeBuffer.AwaitReply);
                }

                _dispatcher.AddResult(ar, (iar, o) => { this.WriteCallback(iar); }, null);
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
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isConnected = false;

                if (disposing)
                {
                    if (_udpClient != null)
                    {
                        _udpClient.Close();
                    }
                }

                _udpClient = null;
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

            var awaited = (Boolean)asyncResult.AsyncState;

            byte[] datagram = null;

            try
            {
                if (!_isConnected)
                {
                    if (_udpClient != null)
                    {
                        _udpClient.Close();
                        _udpClient = null;
                    }

                    return;
                }

                datagram = _udpClient.EndReceive(asyncResult, ref _remoteEndPoint);
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
                _protocol.DataReceived(_remoteEndPoint, datagram, 0, datagram.Length);
                _remoteEndPoint = null;
            }
            catch (Exception ex)
            {
                base.OnProtocolException(ex);
            }

            if (awaited)
            {
                _onTransmitCompleteCallback();
            }
            else
            {
                this.Read(false);
            }
        }

        private void WriteCallback(IAsyncResult ar)
        {
            try
            {
                _udpClient.EndSend(ar);
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