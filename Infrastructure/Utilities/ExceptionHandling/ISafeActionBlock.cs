using System;

namespace Controls.ExceptionHandling
{
    public interface ISafeActionBlock
    {
        void Invoke(Action @do, Action onFailureDo, Action @finally);
    }
}