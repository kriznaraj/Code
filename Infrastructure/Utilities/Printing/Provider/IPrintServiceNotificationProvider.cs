using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Printing
{
    public interface IPrintServiceNotificationProvider : IPrintManagerNotificationHandler
    {
        AutoResetEvent AddNotification(PrintJobId printJobId);

        void AddSubscription(PrintJobId printJobId, IPrintManagerNotificationHandler printManagerNotificationHandler);

        IPrintResult GetResponse(PrintJobId printJobId);

        void RemoveSubscription(PrintJobId printJobId);
    }
}