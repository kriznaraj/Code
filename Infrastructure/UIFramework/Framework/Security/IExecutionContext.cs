using System;
using System.Collections.Generic;
using System.Globalization;
using Controls.ExceptionHandling;

namespace Controls.Framework
{
    
    public interface IExecutionContext 
    {
        int TransactionCodeId { get; } //CurrentUserOpCode;

        Dictionary<string, string> ResponseHeader { get; }

        ISafeActionBlock SafeActionBlock { get; }
    }
}
