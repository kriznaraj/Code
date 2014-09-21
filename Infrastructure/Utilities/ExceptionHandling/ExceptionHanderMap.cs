using System.Collections.Generic;
using ExceptionFullName = System.String;

namespace BallyTech.Infrastructure.ExceptionHandling
{
    internal sealed class ExceptionHandlerMap
        : Dictionary<ExceptionFullName, SortedExceptionHandlerList> 
    { 
    }
}
