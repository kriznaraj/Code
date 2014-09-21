using System;
using System.Collections.Generic;

namespace BallyTech.Infrastructure.Communication
{
    internal interface ITransmitter<T>
    {
        void Transmit(T data, Action onComplete);
    }

    internal class TransmissionQueue<T>
    {
        private Boolean _transmitting;
        private Queue<T> _transmissionQueue;
        private readonly ITransmitter<T> _transmitter;

        public TransmissionQueue(ITransmitter<T> transmitter)
        {
            _transmitting = false;
            _transmitter = transmitter;
            _transmissionQueue = new Queue<T>();
        }

        public void Enqueue(T data)
        {
            _transmissionQueue.Enqueue(data);
            this.BeginTransmit();
        }

        private void BeginTransmit()
        {
            if (!_transmitting && _transmissionQueue.Count > 0)
            {
                _transmitting = true;
                var next = _transmissionQueue.Dequeue();
                _transmitter.Transmit(next, this.OnTransmitComplete);
            }
        }

        private void OnTransmitComplete()
        {
            _transmitting = false;
            this.BeginTransmit();
        }
    }
}