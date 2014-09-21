using System.Collections.Generic;
using ExceptionFullName = System.String;

namespace Controls.ExceptionHandling
{
    internal class ExceptionMap<T, K>
        : Dictionary<ExceptionFullName, SortedExceptionConfigList<T, K>>
    {
    }
}