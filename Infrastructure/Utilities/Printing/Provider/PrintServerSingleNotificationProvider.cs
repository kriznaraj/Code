using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintServerSingleNotificationProvider : PrintServiceNotificationProvider
    {
        private readonly IPrintManagerNotificationHandler handler;
        private readonly ConcurrentDictionary<PrintJobId, byte> printJobIds;

        public PrintServerSingleNotificationProvider(IPrintManagerNotificationHandler handler, IPrintServiceNotify notify)
            : base(notify)
        {
            this.handler = handler;
            this.printJobIds = new ConcurrentDictionary<PrintJobId, byte>();
        }

        protected override void AddSubscriptionImpl(PrintJobId printJobId, IPrintManagerNotificationHandler printManagerNotificationHandler)
        {
            this.printJobIds[printJobId] = 0;
        }

        protected override void RemoveSubscriptionImpl(PrintJobId printJobId)
        {
            byte ret;
            this.printJobIds.TryRemove(printJobId, out ret);
        }

        protected override bool TryGetNotificationHandler(PrintJobId printJobId, out IPrintManagerNotificationHandler handler)
        {
            bool itemFound = false;
            handler = null;
            if (this.printJobIds.ContainsKey(printJobId))
            {
                handler = this.handler;
                itemFound = true;
            }

            return itemFound;
        }
    }
}