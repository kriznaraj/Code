using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single)]
    internal class PrintManagerCallback : IPrintManagerCallback
    {
        private readonly IPrintManagerNotificationHandler handler;

        public PrintManagerCallback(IPrintManagerNotificationHandler handler)
        {
            this.handler = handler;
        }

        void IPrintManagerCallback.NotifyFailure(PrintJobId printJobId, int reason, string reasonCode)
        {
            this.handler.OnPrintFailed(printJobId, new PrintErrorResult(printJobId) { Status = PrintStatus.Failed, ReasonCode = reasonCode, Reason = (FailReason)reason });
        }

        void IPrintManagerCallback.NotifySuccess(PrintJobId printJobId)
        {
            this.handler.OnPrintCompleted(printJobId, new PrintSuccessResult(printJobId) { PrintedDateTime = DateTimeOffset.Now, Status = PrintStatus.Success });
        }
    }
}