using System;
using Controls.Logging;

namespace Controls.ExceptionHandling
{
    internal sealed class SafeActionBlock : ISafeActionBlock
    {
        private readonly SafeActionReturnBlock safeBlock;

        internal SafeActionBlock(ILogger logger, IExceptionManager exceptionManager, int retryCount)
        {
            this.safeBlock = new SafeActionReturnBlock(logger, exceptionManager, retryCount);
        }

        void ISafeActionBlock.Invoke(Action @do, Action onFailureDo, Action @finally)
        {
            this.safeBlock.Invoke<object>(@do, onFailureDo, @finally, () => null);
        }
    }
}