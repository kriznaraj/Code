using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal sealed class PrintSuccessResult : PrintResult, IPrintSuccessResult
    {
        internal PrintSuccessResult(PrintJobId printJobId)
            : base(printJobId)
        {
        }

        public DateTimeOffset PrintedDateTime
        {
            get;
            set;
        }
    }
}