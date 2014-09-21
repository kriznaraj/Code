using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal sealed class PrintErrorResult : PrintResult, IPrintErrorResult
    {
        internal PrintErrorResult(PrintJobId printJobId)
            : base(printJobId)
        {
        }

        public FailReason Reason
        {
            get;
            set;
        }

        public string ReasonCode
        {
            get;
            set;
        }
    }
}