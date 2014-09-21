using System.Collections.Generic;

namespace BallyTech.Infrastructure.ExceptionHandling
{
    internal sealed class SortedExceptionHandlerList : 
        SortedList<int, KeyValuePair<ExceptionHandlerConfig, IExceptionHandler>> 
    { 
    }
}
