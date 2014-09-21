using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controls.ExceptionHandling;

namespace Controls.Framework
{
    internal class ExecutionContext : IExecutionContext
    {
        public ExecutionContext()
        {
            ResponseHeader = new Dictionary<string, string>();
        }
        
        public int TransactionCodeId
        {
            get;
            set;
        }

        public Dictionary<string, string> ResponseHeader
        {
            get;
            set;
        }

        public ISafeActionBlock SafeActionBlock
        {
            get;
            set;
        }
    }
}
