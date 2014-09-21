using Controls.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintManagerAsyncProvider : IPrintManagerProvider
    {
        private readonly PrintManagerChannelFactory printManagerChannelFactory;
        private readonly IPrintManagerNotificationHandler printManagerNotificationHandler;
        private readonly IPrintServiceNotificationProvider printServiceNotificationProvider;

        public PrintManagerAsyncProvider(
            PrintManagerChannelFactory printManagerChannelFactory,
            IPrintServiceNotificationProvider printServiceNotificationProvider,
            IPrintManagerNotificationHandler printManagerNotificationHandler)
        {
            this.printManagerChannelFactory = printManagerChannelFactory;
            this.printManagerNotificationHandler = printManagerNotificationHandler;
            this.printServiceNotificationProvider = printServiceNotificationProvider;
        }

        public IPrintResult Print(IPrintData printData)
        {
            this.printServiceNotificationProvider.AddSubscription(printData.PrintJobIInfo, this.printManagerNotificationHandler);
            IPrintManager printManager = this.printManagerChannelFactory.CreateManager(printData.PrintJobIInfo.Key);
            try
            {
                printManager.Print(printData.PrintJobIInfo, printData.Source, printData.Settings, printData.GetDatasource());
                return null;
            }
            catch (CommunicationException communicationException)
            {
                this.printServiceNotificationProvider.RemoveSubscription(printData.PrintJobIInfo);
                return new PrintErrorResult(printData.PrintJobIInfo) { Reason = FailReason.ServiceUnavailable, ReasonCode = communicationException.GetExceptionMessage() };
            }
        }
    }
}