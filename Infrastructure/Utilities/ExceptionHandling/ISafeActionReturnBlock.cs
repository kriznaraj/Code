using System;

namespace Controls.ExceptionHandling
{
    public interface ISafeActionReturnBlock
    {
        TResult Invoke<TResult>(Action @do, Action onFailureDo, Action @finally, Func<TResult> resultFactory);
    }
}