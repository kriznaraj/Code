using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Communication
{
    /// <summary>
    /// Transport, Protocol agnostic message dispatcher based on Reactor pattern.
    /// </summary>
    internal sealed class Reactor : IReactor
    {
        private readonly List<ReactorSlot> _slots = new List<ReactorSlot>();
        private Boolean _stop = false;

        private AutoResetEvent _loopControl = new AutoResetEvent(false);

        void IReactor.AddResult(IAsyncResult ar, Action<IAsyncResult, object> callback, object state)
        {
            var slot = new ReactorSlot()
            {
                Result = ar,
                Callback = callback,
                Handle = ar.AsyncWaitHandle,
                State = state
            };

            _slots.Add(slot);

            _loopControl.Set();
        }

        void IReactor.Run()
        {
            Task.Factory.StartNew(
                () =>
                {
                    while (!_stop)
                    {
                        this.Loop();

                        if (_slots.Count == 0)
                            _loopControl.WaitOne();
                    }
                });
        }

        void IReactor.Shutdown()
        {
            lock (this)
            {
                _stop = true;
            }
        }

        private void Loop()
        {
            if (_slots.Count == 0)
            {
                return;
            }

            TimeSpan timeout = new TimeSpan(-1);

            try
            {
                var slotCount = _slots.Count;

                var handles = new WaitHandle[slotCount];

                for (int ctr = 0; ctr < _slots.Count; ctr++)
                {
                    handles[ctr] = _slots[ctr].Handle;
                }

                int signalledSlotIndex = WaitHandle.WaitAny(handles, timeout, false);
                if (signalledSlotIndex != WaitHandle.WaitTimeout)
                {
                    var signalledSlot = _slots[signalledSlotIndex];
                    _slots.RemoveAt(signalledSlotIndex);

                    try
                    {
                        signalledSlot.Callback(signalledSlot.Result, signalledSlot.State);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
    }
}