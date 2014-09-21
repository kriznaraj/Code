using System;
using System.Threading;

namespace BallyTech.Infrastructure.Communication
{
    internal sealed class ReactorSlot
    {
        internal IAsyncResult Result;
        internal object State;
        internal WaitHandle Handle;
        internal Action<IAsyncResult, Object> Callback;
    }
}