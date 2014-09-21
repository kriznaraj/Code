using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintResult : IPrintResult
    {
        public PrintResult(PrintJobId printJobId)
        {
            this.PrintJobId = printJobId;
        }

        public PrintJobId PrintJobId
        {
            get;
            private set;
        }

        public PrintStatus Status
        {
            get;
            set;
        }
    }
}