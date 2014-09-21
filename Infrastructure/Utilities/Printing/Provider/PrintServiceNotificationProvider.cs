using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal abstract class PrintServiceNotificationProvider : IPrintServiceNotificationProvider
    {
        private readonly IPrintServiceNotify notify;
        private readonly ConcurrentDictionary<PrintJobId, AutoResetEvent> printJobResetEvent;
        private readonly ConcurrentDictionary<PrintJobId, IPrintResult> resultDictionary;

        public PrintServiceNotificationProvider(IPrintServiceNotify printServiceNotify)
        {
            this.notify = printServiceNotify;
            this.printJobResetEvent = new ConcurrentDictionary<PrintJobId, AutoResetEvent>();
            this.resultDictionary = new ConcurrentDictionary<PrintJobId, IPrintResult>();
        }

        public AutoResetEvent AddNotification(PrintJobId printJobId)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            this.printJobResetEvent[printJobId] = resetEvent;
            return resetEvent;
        }

        public void AddSubscription(PrintJobId printJobId, IPrintManagerNotificationHandler printManagerNotificationHandler)
        {
            this.AddSubscriptionImpl(printJobId, printManagerNotificationHandler);
        }

        public IPrintResult GetResponse(PrintJobId printJobId)
        {
            IPrintResult result;
            if (false == this.resultDictionary.TryRemove(printJobId, out result))
            {
                result = new PrintResult(printJobId) { Status = PrintStatus.Pending };
            }

            return result;
        }

        public void OnPrintCompleted(PrintJobId printJobId, IPrintSuccessResult printResponse)
        {
            if (false == this.NotifyEvent(printJobId, printResponse))
            {
                if (false == this.PublishSuccessEvent(printJobId, printResponse))
                {
                    this.notify.NotifyHandlerNotFound(printResponse);
                }
            }
        }

        public void OnPrintFailed(PrintJobId printJobId, IPrintErrorResult printResponse)
        {
            if (false == this.NotifyEvent(printJobId, printResponse))
            {
                if (false == this.PublishFailedEvent(printJobId, printResponse))
                {
                    this.notify.NotifyHandlerNotFound(printResponse);
                }
            }
        }

        public void RemoveSubscription(PrintJobId printJobId)
        {
            this.RemoveSubscriptionImpl(printJobId);
        }

        protected abstract void AddSubscriptionImpl(PrintJobId printJobId, IPrintManagerNotificationHandler printManagerNotificationHandler);

        protected abstract void RemoveSubscriptionImpl(PrintJobId printJobId);

        protected abstract bool TryGetNotificationHandler(PrintJobId printJobId, out IPrintManagerNotificationHandler handler);

        private bool NotifyEvent(PrintJobId printJobId, IPrintResult result)
        {
            bool eventNotified = false;
            AutoResetEvent resetEvent;
            if (this.printJobResetEvent.TryGetValue(printJobId, out resetEvent))
            {
                eventNotified = true;
                this.resultDictionary[printJobId] = result;
                resetEvent.Set();
            }

            return eventNotified;
        }

        private bool PublishFailedEvent(PrintJobId printJobId, IPrintErrorResult result)
        {
            bool eventPublished = false;
            IPrintManagerNotificationHandler handler;
            if (this.TryGetNotificationHandler(printJobId, out handler))
            {
                handler.OnPrintFailed(printJobId, result);
                eventPublished = true;
            }

            return eventPublished;
        }

        private bool PublishSuccessEvent(PrintJobId printJobId, IPrintSuccessResult result)
        {
            bool eventPublished = false;
            IPrintManagerNotificationHandler handler;
            if (this.TryGetNotificationHandler(printJobId, out handler))
            {
                handler.OnPrintCompleted(printJobId, result);
                eventPublished = true;
            }

            return eventPublished;
        }
    }
}