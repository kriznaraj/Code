using Controls.Configuration;
using Controls.Logging;
using Controls.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintService : IPrintService, IPrintServiceNotify, IPrintManagerNotificationHandler
    {
        private readonly PrintManagerFactory factory;
        private readonly ConcurrentDictionary<PrintJobId, PrintResponse> printResponseDictionary;

        public PrintService(PrintServiceConfig printServiceConfig, ILogger logger)
        {
            IPrintServiceNotificationProvider provider = new PrintServerSingleNotificationProvider(this, this);
            this.factory = new PrintManagerFactory(printServiceConfig, logger, provider);
            this.printResponseDictionary = new ConcurrentDictionary<PrintJobId, PrintResponse>();
        }

        public event Action<IPrintResult> OnResponseHandlerNotFound;

        public void NotifyHandlerNotFound(IPrintResult printResponse)
        {
            if (null != this.OnResponseHandlerNotFound)
            {
                this.OnResponseHandlerNotFound(printResponse);
            }
        }

        public void OnPrintCompleted(PrintJobId printJobId, IPrintSuccessResult printResponse)
        {
            PrintResponse printServiceResponse;
            if (true == this.printResponseDictionary.TryRemove(printJobId, out printServiceResponse))
            {
                printServiceResponse.OnPrintCompleted(printJobId, printResponse);
            }
            else
            {
                this.NotifyHandlerNotFound(printResponse);
            }
        }

        public void OnPrintFailed(PrintJobId printJobId, IPrintErrorResult printResponse)
        {
            PrintResponse printServiceResponse;
            if (true == this.printResponseDictionary.TryRemove(printJobId, out printServiceResponse))
            {
                printServiceResponse.OnPrintFailed(printJobId, printResponse);
            }
            else
            {
                this.NotifyHandlerNotFound(printResponse);
            }
        }

        public IPrintResult Print(IPrintData printData)
        {
            if (printData == null)
            {
                throw new ArgumentNullException("printData", "Print Data supplied is null");
            }

            if (printData.Settings == null)
            {
                throw new ArgumentNullException("printData.Settings", "Printer Settings supplied is null");
            }

            if (printData.PrintJobIInfo == null)
            {
                throw new ArgumentNullException("printData.PrintJobInfo", "Print Job Id is Null");
            }

            IPrintManagerProvider provider = this.factory.GetPrintManager(printData.PrintJobIInfo.Key, PrintMode.Sync);
            return provider.Print(printData);
        }

        public void PrintAsync(IPrintData printData, Action<IPrintSuccessResult> onPrintSuccess = null, Action<IPrintErrorResult> onPrintFailure = null)
        {
            if (printData == null)
            {
                throw new ArgumentNullException("printData", "Print Data supplied is null");
            }

            if (printData.Settings == null)
            {
                throw new ArgumentNullException("printData.Settings", "Printer Settings supplied is null");
            }

            if (printData.PrintJobIInfo == null)
            {
                throw new ArgumentNullException("printData.PrintJobInfo", "Print Job Id is Null");
            }

            PrintResponse response = new PrintResponse(printData.PrintJobIInfo, onPrintSuccess, onPrintFailure);
            IPrintManagerProvider provider = this.factory.GetPrintManager(printData.PrintJobIInfo.Key, PrintMode.Async);
            this.printResponseDictionary[printData.PrintJobIInfo] = response;
            provider.Print(printData);
        }
    }
}