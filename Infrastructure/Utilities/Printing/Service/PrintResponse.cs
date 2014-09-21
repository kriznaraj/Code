using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintResponse : IPrintManagerNotificationHandler
    {
        private Action<IPrintErrorResult> onPrintFailure;

        private Action<IPrintSuccessResult> onPrintSuccess;

        internal PrintResponse(PrintJobId printJobId, Action<IPrintSuccessResult> onPrintSuccess, Action<IPrintErrorResult> onPrintFailure)
        {
            this.PrintJobId = printJobId;
            this.onPrintSuccess = onPrintSuccess;
            this.onPrintFailure = onPrintFailure;
        }

        public PrintJobId PrintJobId
        {
            get;
            private set;
        }

        public void OnPrintCompleted(PrintJobId printJobId, IPrintSuccessResult printResponse)
        {
            if (null != this.onPrintSuccess)
            {
                this.onPrintSuccess(printResponse);
            }
        }

        public void OnPrintFailed(PrintJobId printJobId, IPrintErrorResult printResponse)
        {
            if (null != this.onPrintFailure)
            {
                this.onPrintFailure(printResponse);
            }
        }
    }
}