using Controls.Types;
using Controls.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintManagerSyncProvider : IPrintManagerProvider
    {
        private readonly PrintManagerChannelFactory printManagerChannelFactory;
        private readonly IPrintServiceNotificationProvider printServiceNotificationProvider;

        public PrintManagerSyncProvider(PrintManagerChannelFactory printManagerChannelFactory, IPrintServiceNotificationProvider printServiceNotificationProvider)
        {
            this.printManagerChannelFactory = printManagerChannelFactory;
            this.printServiceNotificationProvider = printServiceNotificationProvider;
        }

        public IPrintResult Print(IPrintData printData)
        {
            AutoResetEvent autoResetEvent = this.printServiceNotificationProvider.AddNotification(printData.PrintJobIInfo);
            IPrintManager printManager = this.printManagerChannelFactory.CreateManager(printData.PrintJobIInfo.Key);
            try
            {
                printManager.Print(printData.PrintJobIInfo, printData.Source, printData.Settings, printData.GetDatasource());
                if (autoResetEvent.WaitOne(printData.Settings.Timeout))
                {
                    return this.printServiceNotificationProvider.GetResponse(printData.PrintJobIInfo);
                }
                else
                {
                    return new PrintErrorResult(printData.PrintJobIInfo) { Reason = FailReason.Timeout, ReasonCode = "Service Timeout. No Response returned after given time" };
                }
            }
            catch (CommunicationException communicationException)
            {
                return new PrintErrorResult(printData.PrintJobIInfo) { Reason = FailReason.ServiceUnavailable, ReasonCode = communicationException.GetExceptionMessage() };
            }
        }
    }
}