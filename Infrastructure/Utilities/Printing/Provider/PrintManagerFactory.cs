using Controls.Configuration;
using Controls.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal enum PrintMode
    {
        Sync,
        Async
    }

    internal class PrintManagerFactory
    {
        private readonly Dictionary<string, Dictionary<PrintMode, IPrintManagerProvider>> printManagerProviderDictionary;

        public PrintManagerFactory(PrintServiceConfig printServiceConfig, ILogger logger, IPrintServiceNotificationProvider provider)
        {
            PrintManagerProviderFactory factory = new PrintManagerProviderFactory(printServiceConfig, new PrintManagerChannelFactory(printServiceConfig), provider);
            this.printManagerProviderDictionary = new Dictionary<string, Dictionary<PrintMode, IPrintManagerProvider>>();
            foreach (PrintManagerConfig config in printServiceConfig.PrinterConfig)
            {
                string key = string.Empty;
                switch (config.PrintManagerType)
                {
                    case PrintManagerType.BallyCentralPrintManager:
                        key = "PM";
                        break;

                    default:
                    case PrintManagerType.None:
                        throw new NotSupportedException("config.PrintManagerType not Supported");
                }

                Dictionary<PrintMode, IPrintManagerProvider> printManagerProvider = new Dictionary<PrintMode, IPrintManagerProvider>();
                foreach (var item in Enum.GetValues(typeof(PrintMode)).OfType<PrintMode>())
                {
                    printManagerProvider[item] = factory.GetPrintManager(key, item);
                }

                this.printManagerProviderDictionary[config.Key] = printManagerProvider;
            }

            factory.StartListeners();
        }

        internal IPrintManagerProvider GetPrintManager(string key, PrintMode printMode)
        {
            IPrintManagerProvider provider = null;
            Dictionary<PrintMode, IPrintManagerProvider> printManagerProvider;
            if (false == this.printManagerProviderDictionary.TryGetValue(key, out printManagerProvider))
            {
                printManagerProvider = this.printManagerProviderDictionary["DEFAULT"];
            }

            if (false == printManagerProvider.TryGetValue(printMode, out provider))
            {
                throw new NotImplementedException("Print Manager provider for key= '" + key + "' and Print Mode '" + printMode + "' not implemented. Check configuration");
            }

            return provider;
        }
    }
}