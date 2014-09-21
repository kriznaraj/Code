using System.Collections.Generic;

namespace Controls.ExceptionHandling
{
    internal sealed class SortedExceptionConfigList<T, K> :
        SortedList<int, KeyValuePair<T, K>>
    {
    }
}